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

            return source.Zip(other, (x, y) => x.Equals(y)).All(x => x);
        }

        public static bool AllItemsMatch<T>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return source.Zip(other, (x, y) => predicate(x, y)).All(x => x);
        }

        public static IEnumerable<T> Convolve<T>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, T, T> accumulator) where T : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            other = other.Reverse();
            int length1 = source.Count();
            int length2 = other.Count();
            T accumulatedValue = new T();
            for(int i = 0; i < length1 + length2; i++)
            {
                for(int j = 0; j < i; j++)
                {
                    accumulatedValue = accumulator(source.ElementAtOrDefault(j), other.ElementAtOrDefault(j));
                }
                yield return accumulatedValue;
            }
        }
    }
}
