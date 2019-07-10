using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordManagerInterfaces
{
    public static class PMALinqExtensions
    {
        /// <summary>
        /// Ensures that each item in an ordered collection is equal to the same ordered item in another collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns>True if all items in order in the collection are equal, using the default Equals comparer</returns>
        public static bool AllItemsAreEqual<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            IEnumerator<T> sourceEnumerator = source.GetEnumerator();
            IEnumerator<T> otherEnumerator = other.GetEnumerator();
            bool sourceActive = sourceEnumerator.MoveNext();
            bool otherActive = otherEnumerator.MoveNext();
            while(sourceActive && otherActive)
            {
                if (!sourceEnumerator.Current.Equals(otherEnumerator.Current))
                    return false;
                sourceActive = sourceEnumerator.MoveNext();
                otherActive = otherEnumerator.MoveNext();
                if (sourceActive != otherActive)
                    return false;
            }
            return true;
        }

        public static bool AllItemsMatch<T>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            IEnumerator<T> sourceEnumerator = source.GetEnumerator();
            IEnumerator<T> otherEnumerator = other.GetEnumerator();
            bool sourceActive = sourceEnumerator.MoveNext();
            bool otherActive = otherEnumerator.MoveNext();
            while (sourceActive && otherActive)
            {
                if (!predicate(sourceEnumerator.Current, otherEnumerator.Current))
                    return false;
                sourceActive = sourceEnumerator.MoveNext();
                otherActive = otherEnumerator.MoveNext();
                if (sourceActive != otherActive)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// This method combines information from an item in one collection with every item in another collection,
        /// using an accumulator to combine the information.  This is intended to encode a new password from
        /// known information without the generated password being reasonable to reverse engineer the source information from.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="accumulator"></param>
        /// <returns></returns>
        public static IEnumerable<T> Mingle<T>(this IEnumerable<T> source, IEnumerable<T> other, Accumulator<T> accumulator) where T : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            other = other.Reverse();
            int length1 = source.Count();
            int length2 = other.Count();
            
            for(int i = 0; i < length1 + length2 - 1; i++)
            {
                accumulator.Reset();
                for (int j = 0; j < length2; j++)
                {
                    accumulator.Accumulate(source.ElementAtOrDefault(i - j), other.ElementAtOrDefault(j));
                }
                yield return accumulator.AccumulatedValue;

            }
        }
    }
}
