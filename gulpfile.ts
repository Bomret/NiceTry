import { yellow } from 'chalk'
import del from 'del'
import log from 'fancy-log'
import gulp from 'gulp'
import { serial } from 'gulp-fun'
import shell from 'gulp-shell'
import tap from 'gulp-tap'
import mv from 'mv'
import path from 'path'
import { env } from 'process'

const branch = (function () {
  if (!env.BRANCH) return null

  const nameParts = env.BRANCH.split('/')

  return nameParts[nameParts.length - 1]
})()

const sourceDir = path.join(__dirname, 'src')
const testsDir = path.join(__dirname, 'test')
const artifactsDir = path.join(__dirname, 'artifacts')
const distDir = path.join(__dirname, 'dist')

const clean = () => del([artifactsDir, distDir])

gulp.task('dotnetRestore', shell.task(['dotnet restore --nologo']))

const dotnetCompile = () =>
  gulp.src(`${sourceDir}/**/*.*proj`, { read: false }).pipe(
    tap((file, t: any) => {
      const outputDir = path.join(artifactsDir, path.relative(sourceDir, file.dirname))

      const args = ['dotnet build', file.path, '--output', outputDir, '--no-restore', '--nologo']

      return t.through(shell, [args.join(' ')])
    })
  )

const dotnetTest = () =>
  gulp.src(`${testsDir}/**/*.*proj`, { read: false }).pipe(
    tap((file, t: any) => {
      const args = ['dotnet test', file.path, '--no-restore', '--nologo']

      return t.through(shell, [args.join(' ')])
    })
  )

const compileDocs = () =>
  gulp.src(`${sourceDir}/**/README.md`, { read: false }).pipe(
    tap(async (file, t: any) => {
      const args = ['npx', '--yes', '--', 'md-to-pdf', '--config-file', './docs-config.json', file.path]

      return t.through(shell, [args.join(' ')]).pipe(
        serial(async (file, stream) => {
          const parentDir = path.relative(sourceDir, file.dirname)
          const outputDir = path.join(distDir, parentDir)

          const pdfFilename = file.basename.replace(path.extname(file.basename), '.pdf')
          const currentPdfFilepath = path.join(file.dirname, pdfFilename)

          const outputPdfFilename = `${parentDir}.pdf`
          const targetPdfFilepath = path.join(outputDir, outputPdfFilename)

          await new Promise<void>((resolve, reject) =>
            mv(currentPdfFilepath, targetPdfFilepath, { mkdirp: true }, (err) => {
              if (err) return reject(err)

              return resolve()
            })
          )

          stream.push(file)
        })
      )
    })
  )

const dotnetPack = () =>
  gulp.src(`${sourceDir}/**/*.*proj`, { read: false }).pipe(
    tap((file, t: any) => {
      const outputDir = path.join(distDir, path.relative(sourceDir, file.dirname))

      const args = [
        'dotnet pack',
        file.path,
        '--configuration Release',
        '--output',
        outputDir,
        '--no-restore',
        '--nologo',
      ]

      return t.through(shell, [args.join(' ')])
    })
  )

const dotnetPush = () =>
  gulp.src(`${distDir}/**/*.nupkg`, { read: false }).pipe(
    tap((file, t: any) => {
      if (branch !== 'main') {
        log(yellow(`${file.basename}: push to NuGet is disabled for branch ${branch}`))

        return
      }

      const args = [
        'dotnet nuget push',
        '<%= file.path %>',
        '--source',
        'github',
        '--api-key',
        env.NUGET_TOKEN,
        '--skip-duplicate',
      ]

      return t.through(shell, [args.join(' ')])
    })
  )

export const build = gulp.series(clean, 'dotnetRestore', dotnetCompile)
export const test = gulp.series('dotnetRestore', dotnetTest)
export const docs = gulp.series(compileDocs)
export const publish = gulp.series(clean, test, compileDocs, dotnetPack, dotnetPush)
