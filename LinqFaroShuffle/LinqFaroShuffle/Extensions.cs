using System;
using System.Collections.Generic;
using System.Text;

namespace LinqFaroShuffle;

public static class CardExtensions
{
    extension<T>(IEnumerable<T> sequence)
    {
        public IEnumerable<T> InterleaveSequenceWith(IEnumerable<T> second)
        {
            var firstIter = sequence.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while (firstIter.MoveNext() && secondIter.MoveNext())
            {
                yield return firstIter.Current;
                yield return secondIter.Current;
            }
        }

    }
}