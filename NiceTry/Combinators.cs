using System;

namespace NiceTry {
    public static class Combinators {
        public static ITry<TB> Then<TA, TB>(this ITry<TA> ta, Func<ITry<TA>, ITry<TB>> func) {
            return ta.IsFailure
                ? new Failure<TB>(ta.Error)
                : func(ta);
        }

        public static ITry<T> Then<T>(this ITry t, Func<ITry, ITry<T>> func) {
            return t.IsFailure
                ? new Failure<T>(t.Error)
                : func(t);
        }

        public static ITry Then<T>(this ITry<T> t, Func<ITry<T>, ITry> func) {
            return t.IsFailure
                ? new Failure(t.Error)
                : func(t);
        }

        public static ITry Then(this ITry t, Func<ITry, ITry> func) {
            return t.IsFailure
                ? new Failure(t.Error)
                : func(t);
        }

        public static ITry<T> OrElse<T>(this ITry<T> t, Func<ITry<T>> func) {
            return t.IsFailure
                ? func()
                : t;
        }

        public static ITry<T> OrElse<T>(this ITry<T> t, ITry<T> et) {
            return t.IsFailure
                ? et
                : t;
        }

        public static ITry Apply<T>(this ITry<T> t, Action<T> action) {
            return t.FlatMap(x => Try.To(() => action(x)));
        }

        public static ITry<TB> Map<TA, TB>(this ITry<TA> ta, Func<TA, TB> func) {
            return ta.FlatMap(a => Try.To(() => func(a)));
        }

        public static ITry<TB> FlatMap<TA, TB>(this ITry<TA> ta, Func<TA, ITry<TB>> func) {
            return ta.IsFailure
                ? new Failure<TB>(ta.Error)
                : func(ta.Value);
        }

        public static ITry FlatMap<T>(this ITry<T> t, Func<T, ITry> func) {
            return t.IsFailure
                ? new Failure(t.Error)
                : func(t.Value);
        }

        public static ITry<TC> LiftMap<TA, TB, TC>(this ITry<TA> ta, ITry<TB> tb, Func<TA, TB, TC> func) {
            return ta.FlatMap(a => tb.Map(b => func(a, b)));
        }

        public static ITry<TD> LiftMap<TA, TB, TC, TD>(this ITry<TA> ta, ITry<TB> tb, ITry<TC> tc,
            Func<TA, TB, TC, TD> func) {
            return ta.FlatMap(a => tb.FlatMap(b => tc.Map(c => func(a, b, c))));
        }

        public static ITry<TE> LiftMap<TA, TB, TC, TD, TE>(this ITry<TA> ta, ITry<TB> tb, ITry<TC> tc, ITry<TD> td,
            Func<TA, TB, TC, TD, TE> func) {
            return ta.FlatMap(a => tb.FlatMap(b => tc.FlatMap(c => td.Map(d => func(a, b, c, d)))));
        }

#if NET40
        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TResult>(this ITry<TA> ta,
                                                                         ITry<TB> tb,
                                                                         ITry<TC> tc,
                                                                         ITry<TD> td,
                                                                         ITry<TE> te,
                                                                         Func<TA, TB, TC, TD, TE, TResult> func)
        {
            return ta.FlatMap(a => tb.FlatMap(b => tc.FlatMap(c => td.FlatMap(d => te.Map(e => func(a, b, c, d, e))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TResult>(this ITry<TA> ta,
                                                                             ITry<TB> tb,
                                                                             ITry<TC> tc,
                                                                             ITry<TD> td,
                                                                             ITry<TE> te,
                                                                             ITry<TF> tf,
                                                                             Func<TA, TB, TC, TD, TE, TF, TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b => tc.FlatMap(c => td.FlatMap(d => te.FlatMap(e => tf.Map(f => func(a, b, c, d, e, f)))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TResult>(this ITry<TA> ta,
                                                                                 ITry<TB> tb,
                                                                                 ITry<TC> tc,
                                                                                 ITry<TD> td,
                                                                                 ITry<TE> te,
                                                                                 ITry<TF> tf,
                                                                                 ITry<TG> tg,
                                                                                 Func
                                                                                     <TA, TB, TC, TD, TE, TF, TG,
                                                                                     TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(d => te.FlatMap(e => tf.FlatMap(f => tg.Map(g => func(a, b, c, d, e, f, g))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TResult>(this ITry<TA> ta,
                                                                                     ITry<TB> tb,
                                                                                     ITry<TC> tc,
                                                                                     ITry<TD> td,
                                                                                     ITry<TE> te,
                                                                                     ITry<TF> tf,
                                                                                     ITry<TG> tg,
                                                                                     ITry<TH> th,
                                                                                     Func
                                                                                         <TA, TB, TC, TD, TE, TF, TG, TH
                                                                                         ,
                                                                                         TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e => tf.FlatMap(f => tg.FlatMap(g => th.Map(h => func(a, b, c, d, e, f, g, h)))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TI, TResult>(this ITry<TA> ta,
                                                                                         ITry<TB> tb,
                                                                                         ITry<TC> tc,
                                                                                         ITry<TD> td,
                                                                                         ITry<TE> te,
                                                                                         ITry<TF> tf,
                                                                                         ITry<TG> tg,
                                                                                         ITry<TH> th,
                                                                                         ITry<TI> ti,
                                                                                         Func
                                                                                             <TA, TB, TC, TD, TE, TF, TG
                                                                                             , TH, TI,
                                                                                             TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e =>
                                    tf.FlatMap(
                                        f =>
                                        tg.FlatMap(g => th.FlatMap(h => ti.Map(i => func(a, b, c, d, e, f, g, h, i))))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TResult>(this ITry<TA> ta,
                                                                                             ITry<TB> tb,
                                                                                             ITry<TC> tc,
                                                                                             ITry<TD> td,
                                                                                             ITry<TE> te,
                                                                                             ITry<TF> tf,
                                                                                             ITry<TG> tg,
                                                                                             ITry<TH> th,
                                                                                             ITry<TI> ti,
                                                                                             ITry<TJ> tj,
                                                                                             Func
                                                                                                 <TA, TB, TC, TD, TE, TF
                                                                                                 , TG
                                                                                                 , TH, TI, TJ,
                                                                                                 TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e =>
                                    tf.FlatMap(
                                        f =>
                                        tg.FlatMap(
                                            g =>
                                            th.FlatMap(
                                                h => ti.FlatMap(i => tj.Map(j => func(a, b, c, d, e, f, g, h, i, j)))))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TResult>(this ITry<TA> ta,
                                                                                                 ITry<TB> tb,
                                                                                                 ITry<TC> tc,
                                                                                                 ITry<TD> td,
                                                                                                 ITry<TE> te,
                                                                                                 ITry<TF> tf,
                                                                                                 ITry<TG> tg,
                                                                                                 ITry<TH> th,
                                                                                                 ITry<TI> ti,
                                                                                                 ITry<TJ> tj,
                                                                                                 ITry<TK> tk,
                                                                                                 Func
                                                                                                     <TA, TB, TC, TD, TE
                                                                                                     , TF, TG
                                                                                                     , TH, TI, TJ, TK,
                                                                                                     TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e =>
                                    tf.FlatMap(
                                        f =>
                                        tg.FlatMap(
                                            g =>
                                            th.FlatMap(
                                                h =>
                                                ti.FlatMap(
                                                    i =>
                                                    tj.FlatMap(j => tk.Map(k => func(a, b, c, d, e, f, g, h, i, j, k))))))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TResult>(this ITry<TA> ta,
                                                                                                     ITry<TB> tb,
                                                                                                     ITry<TC> tc,
                                                                                                     ITry<TD> td,
                                                                                                     ITry<TE> te,
                                                                                                     ITry<TF> tf,
                                                                                                     ITry<TG> tg,
                                                                                                     ITry<TH> th,
                                                                                                     ITry<TI> ti,
                                                                                                     ITry<TJ> tj,
                                                                                                     ITry<TK> tk,
                                                                                                     ITry<TL> tl,
                                                                                                     Func
                                                                                                         <TA, TB, TC, TD
                                                                                                         , TE, TF, TG
                                                                                                         , TH, TI, TJ,
                                                                                                         TK, TL,
                                                                                                         TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e =>
                                    tf.FlatMap(
                                        f =>
                                        tg.FlatMap(
                                            g =>
                                            th.FlatMap(
                                                h =>
                                                ti.FlatMap(
                                                    i =>
                                                    tj.FlatMap(
                                                        j =>
                                                        tk.FlatMap(
                                                            k => tl.Map(l => func(a, b, c, d, e, f, g, h, i, j, k, l)))))))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TResult>(
            this ITry<TA> ta,
            ITry<TB> tb,
            ITry<TC> tc,
            ITry<TD> td,
            ITry<TE> te,
            ITry<TF> tf,
            ITry<TG> tg,
            ITry<TH> th,
            ITry<TI> ti,
            ITry<TJ> tj,
            ITry<TK> tk,
            ITry<TL> tl,
            ITry<TM> tm,
            Func
                <TA, TB, TC, TD, TE, TF, TG
                , TH, TI, TJ, TK, TL, TM,
                TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e =>
                                    tf.FlatMap(
                                        f =>
                                        tg.FlatMap(
                                            g =>
                                            th.FlatMap(
                                                h =>
                                                ti.FlatMap(
                                                    i =>
                                                    tj.FlatMap(
                                                        j =>
                                                        tk.FlatMap(
                                                            k =>
                                                            tl.FlatMap(
                                                                l =>
                                                                tm.Map(m => func(a, b, c, d, e, f, g, h, i, j, k, l, m))))))))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TResult>(
            this ITry<TA> ta,
            ITry<TB> tb,
            ITry<TC> tc,
            ITry<TD> td,
            ITry<TE> te,
            ITry<TF> tf,
            ITry<TG> tg,
            ITry<TH> th,
            ITry<TI> ti,
            ITry<TJ> tj,
            ITry<TK> tk,
            ITry<TL> tl,
            ITry<TM> tm,
            ITry<TN> tn,
            Func
                <TA, TB, TC, TD, TE, TF, TG
                , TH, TI, TJ, TK, TL, TM, TN,
                TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e =>
                                    tf.FlatMap(
                                        f =>
                                        tg.FlatMap(
                                            g =>
                                            th.FlatMap(
                                                h =>
                                                ti.FlatMap(
                                                    i =>
                                                    tj.FlatMap(
                                                        j =>
                                                        tk.FlatMap(
                                                            k =>
                                                            tl.FlatMap(
                                                                l =>
                                                                tm.FlatMap(
                                                                    m =>
                                                                    tn.Map(
                                                                        n =>
                                                                        func(a, b, c, d, e, f, g, h, i, j, k, l, m, n)))))))))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TResult>(
            this ITry<TA> ta,
            ITry<TB> tb,
            ITry<TC> tc,
            ITry<TD> td,
            ITry<TE> te,
            ITry<TF> tf,
            ITry<TG> tg,
            ITry<TH> th,
            ITry<TI> ti,
            ITry<TJ> tj,
            ITry<TK> tk,
            ITry<TL> tl,
            ITry<TM> tm,
            ITry<TN> tn,
            ITry<TO> to,
            Func
                <TA, TB, TC, TD, TE, TF, TG
                , TH, TI, TJ, TK, TL, TM, TN, TO,
                TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e =>
                                    tf.FlatMap(
                                        f =>
                                        tg.FlatMap(
                                            g =>
                                            th.FlatMap(
                                                h =>
                                                ti.FlatMap(
                                                    i =>
                                                    tj.FlatMap(
                                                        j =>
                                                        tk.FlatMap(
                                                            k =>
                                                            tl.FlatMap(
                                                                l =>
                                                                tm.FlatMap(
                                                                    m =>
                                                                    tn.FlatMap(
                                                                        n =>
                                                                        to.Map(
                                                                            o =>
                                                                            func(a, b, c, d, e, f, g, h, i, j, k, l, m,
                                                                                 n, o))))))))))))))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TP, TResult>(
            this ITry<TA> ta,
            ITry<TB> tb,
            ITry<TC> tc,
            ITry<TD> td,
            ITry<TE> te,
            ITry<TF> tf,
            ITry<TG> tg,
            ITry<TH> th,
            ITry<TI> ti,
            ITry<TJ> tj,
            ITry<TK> tk,
            ITry<TL> tl,
            ITry<TM> tm,
            ITry<TN> tn,
            ITry<TO> to,
            ITry<TP> tp,
            Func
                <TA, TB, TC, TD, TE, TF, TG
                , TH, TI, TJ, TK, TL, TM, TN, TO, TP,
                TResult> func)
        {
            return
                ta.FlatMap(
                    a =>
                    tb.FlatMap(
                        b =>
                        tc.FlatMap(
                            c =>
                            td.FlatMap(
                                d =>
                                te.FlatMap(
                                    e =>
                                    tf.FlatMap(
                                        f =>
                                        tg.FlatMap(
                                            g =>
                                            th.FlatMap(
                                                h =>
                                                ti.FlatMap(
                                                    i =>
                                                    tj.FlatMap(
                                                        j =>
                                                        tk.FlatMap(
                                                            k =>
                                                            tl.FlatMap(
                                                                l =>
                                                                tm.FlatMap(
                                                                    m =>
                                                                    tn.FlatMap(
                                                                        n =>
                                                                        to.FlatMap(
                                                                            o =>
                                                                            tp.Map(
                                                                                p =>
                                                                                func(a, b, c, d, e, f, g, h, i, j, k, l,
                                                                                     m, n, o, p)))))))))))))))));
        }
#endif

        public static ITry<T> Flatten<T>(this ITry<ITry<T>> tt) {
            return tt.FlatMap(x => x);
        }

        public static ITry Flatten(this ITry<ITry> tt) {
            return tt.FlatMap(x => x);
        }

        public static ITry Recover(this ITry t, Action<Exception> recover) {
            return t.IsFailure
                ? Try.To(() => recover(t.Error))
                : t;
        }

        public static ITry<T> Recover<T>(this ITry<T> t, Func<Exception, T> func) {
            return t.IsFailure
                ? Try.To(() => func(t.Error))
                : t;
        }

        public static ITry<T> RecoverWith<T>(this ITry<T> t, Func<Exception, ITry<T>> func) {
            return t.IsFailure
                ? func(t.Error)
                : t;
        }

        public static ITry RecoverWith(this ITry t, Func<Exception, ITry> func) {
            return t.IsFailure
                ? func(t.Error)
                : t;
        }

        public static ITry Transform(this ITry t, Func<ITry> whenSuccess, Func<Exception, ITry> whenFailure) {
            return t.IsSuccess
                ? whenSuccess()
                : whenFailure(t.Error);
        }

        public static ITry<TB> Transform<TA, TB>(this ITry<TA> ta, Func<TA, ITry<TB>> whenSuccess,
            Func<Exception, ITry<TB>> whenFailure) {
            return ta.IsSuccess
                ? whenSuccess(ta.Value)
                : whenFailure(ta.Error);
        }

        public static ITry<T> Filter<T>(this ITry<T> t, Func<T, bool> predicate) {
            return t.FlatMap(
                x => predicate(x)
                    ? t
                    : new Failure<T>(
                        new ArgumentException("The given predicate does not hold for this Try.")));
        }
    }
}