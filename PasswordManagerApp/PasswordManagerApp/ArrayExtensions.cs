using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerApp
{
    public static class ArrayExtensions
    {
        public static bool AllItemsAreEqual<T>(this T[] items, T[] compareTo)
        {
            if (items.Length != compareTo.Length)
            {
                return false;
            }

            for (int i = 0; i < items.Length; ++i)
            {
                if(!items[i].Equals(compareTo[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Expands or contracts an array using all the values of the array to generate new values
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="items"></param>
        /// <param name="newLength">The length of the new array</param>
        /// <param name="combinerFunction">A method to combine elements, i.e. (x,y)=>x+y;</param>
        /// <returns></returns>
        public static T[] ReduceOrExpand<T>(this T[] items, int newLength, Func<T,T,T> combinerFunction)
        {
            if(items.Length == newLength)
            {
                return (T[])items.Clone();
            }

            T[] array = new T[newLength];

            //copy the values over into the new array
            for (int i = 0; i < Math.Min(items.Length, newLength); ++i)
            {
                array[i] = items[i];
            }

            if (newLength > items.Length)
            {
                //accumulate the old array and scroll to get the new values
                for(int i = items.Length; i < newLength; ++i)
                {
                    T totalValue = items[i % items.Length];
                    for(int j = i - items.Length; j < i; ++j)
                    {
                        totalValue = combinerFunction(array[j], totalValue);
                    }
                    array[i] = totalValue;
                }
            }
            else
            {
                //accumulate the excess items into the earlier items
                for(int i = 0; i < items.Length - newLength; ++i)
                {
                    array[i % newLength] = combinerFunction(array[i % newLength], items[i + newLength]);
                }
            }

            return array;
        }
    }
}
