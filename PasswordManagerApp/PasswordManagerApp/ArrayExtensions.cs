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
    }
}
