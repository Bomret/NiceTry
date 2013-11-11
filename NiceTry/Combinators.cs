using System;

namespace NiceTry
{
    public static class Combinators
    {
        public static ITry<TNextResult> Then<TResult, TNextResult>(this ITry<TResult> result,
                                                                   Func<ITry<TResult>, ITry<TNextResult>>
                                                                       continuation)
        {
            return result.IsFailure
                       ? new Failure<TNextResult>(result.Error)
                       : continuation(result);
        }

        public static ITry<TNextResult> Then<TNextResult>(this ITry result,
                                                          Func<ITry, ITry<TNextResult>> continuation)
        {
            return result.IsFailure
                       ? new Failure<TNextResult>(result.Error)
                       : continuation(result);
        }

        public static ITry Then<TResult>(this ITry<TResult> result,
                                         Func<ITry<TResult>, ITry> continuation)
        {
            return result.IsFailure
                       ? new Failure(result.Error)
                       : continuation(result);
        }

        public static ITry Then(this ITry result,
                                Func<ITry, ITry> continuation)
        {
            return result.IsFailure
                       ? new Failure(result.Error)
                       : continuation(result);
        }

        public static ITry<TValue> OrElse<TValue>(this ITry<TValue> result,
                                                  Func<ITry<TValue>> orElse)
        {
            return result.IsFailure
                       ? orElse()
                       : result;
        }

        public static ITry<TValue> OrElse<TValue>(this ITry<TValue> result,
                                                  ITry<TValue> orElse)
        {
            return result.IsFailure
                       ? orElse
                       : result;
        }

        public static ITry Apply<TValue>(this ITry<TValue> t, Action<TValue> action)
        {
            return t.IsFailure
                       ? new Failure(t.Error)
                       : Try.To(() => action(t.Value));
        }

        public static ITry<TNewValue> Map<TValue, TNewValue>(this ITry<TValue> t, Func<TValue, TNewValue> func)
        {
            return t.IsFailure
                       ? new Failure<TNewValue>(t.Error)
                       : Try.To(() => func(t.Value));
        }

        public static ITry<TNewValue> FlatMap<TValue, TNewValue>(this ITry<TValue> t, Func<TValue, ITry<TNewValue>> func)
        {
            return t.IsFailure
                       ? new Failure<TNewValue>(t.Error)
                       : func(t.Value);
        }

        public static ITry<TResult> LiftMap<TValue, TNextValue, TResult>(this ITry<TValue> ta,
                                                                         ITry<TNextValue> tb,
                                                                         Func<TValue, TNextValue, TResult> func)
        {
            return ta.IsFailure
                       ? new Failure<TResult>(ta.Error)
                       : ta.FlatMap(a => tb.Map(b => func(a, b)));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TResult>(this ITry<TA> ta,
                                                                 ITry<TB> tb,
                                                                 ITry<TC> tc,
                                                                 Func<TA, TB, TC, TResult> func)
        {
            return ta.FlatMap(a => tb.FlatMap(b => tc.Map(c => func(a, b, c))));
        }

        public static ITry<TResult> LiftMap<TA, TB, TC, TD, TResult>(this ITry<TA> ta,
                                                                     ITry<TB> tb,
                                                                     ITry<TC> tc,
                                                                     ITry<TD> td,
                                                                     Func<TA, TB, TC, TD, TResult> func)
        {
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

        public static ITry<TValue> Flatten<TValue>(this ITry<ITry<TValue>> t)
        {
            return t.IsFailure
                       ? new Failure<TValue>(t.Error)
                       : t.Value;
        }

        public static ITry Flatten(this ITry<ITry> t)
        {
            return t.IsFailure
                       ? new Failure(t.Error)
                       : t.Value;
        }

        public static ITry Recover(this ITry t, Action<Exception> recover)
        {
            return t.IsFailure
                       ? Try.To(() => recover(t.Error))
                       : t;
        }

        public static ITry<TValue> Recover<TValue>(this ITry<TValue> t, Func<Exception, TValue> func)
        {
            return t.IsFailure
                       ? Try.To(() => func(t.Error))
                       : t;
        }

        public static ITry<TValue> RecoverWith<TValue>(this ITry<TValue> t, Func<Exception, ITry<TValue>> func)
        {
            return t.IsFailure
                       ? func(t.Error)
                       : t;
        }

        public static ITry RecoverWith(this ITry t, Func<Exception, ITry> func)
        {
            return t.IsFailure
                       ? func(t.Error)
                       : t;
        }

        public static ITry Transform(this ITry result,
                                     Func<ITry> whenSuccess,
                                     Func<Exception, ITry> whenFailure)
        {
            return result.IsSuccess
                       ? whenSuccess()
                       : whenFailure(result.Error);
        }

        public static ITry<TNewValue> Transform<TValue, TNewValue>(this ITry<TValue> result,
                                                                   Func<TValue, ITry<TNewValue>> whenSuccess,
                                                                   Func<Exception, ITry<TNewValue>> whenFailure)
        {
            return result.IsSuccess
                       ? whenSuccess(result.Value)
                       : whenFailure(result.Error);
        }

        public static ITry<TValue> Filter<TValue>(this ITry<TValue> result,
                                                  Func<TValue, bool> predicate)
        {
            return result.FlatMap(
                v => predicate(v)
                         ? result
                         : new Failure<TValue>(
                               new ArgumentException("The given predicate does not hold for this Try.")));
        }
    }
}