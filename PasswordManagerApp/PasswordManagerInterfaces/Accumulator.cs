using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerInterfaces
{
    /// <summary>
    /// A class used for accumulating data with the IEnumerable.Mingle() method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Accumulator<T> where T : new()
    {
        /// <summary>
        /// Signature for an accumulation method to be used in IEnumerable.Mingle() extension method
        /// </summary>
        /// <param name="accumulatedValue">The value that will be accumulated over</param>
        /// <param name="first">An object from the first collection</param>
        /// <param name="second">An object from the second collection</param>
        /// <returns></returns>
        public delegate T AccumulatorMethod(T accumulatedValue, T first, T second);

        /// <summary>
        /// Method used for accumulation.  See <see cref="AccumulatorMethod"/>.
        /// </summary>
        public AccumulatorMethod AccumulateMethod { get; set; }
        public T AccumulatedValue { get; private set; } = new T();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public T Accumulate(T first, T second)
        {
            AccumulatedValue = AccumulateMethod(AccumulatedValue, first, second);
            return AccumulatedValue;
        }
        public void Reset()
        {
            AccumulatedValue = new T();
        }

        public Accumulator(AccumulatorMethod accumulatorMethod)
        {
            AccumulateMethod = accumulatorMethod ?? throw new ArgumentNullException(nameof(accumulatorMethod));
        }
    }
}
