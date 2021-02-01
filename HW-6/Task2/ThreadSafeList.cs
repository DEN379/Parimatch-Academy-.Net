using System;
using System.Collections.Generic;

namespace Task2
{
    class ThreadSafeList<T>
    {
        private readonly List<T> unsafeList;
        private static readonly object monitor = new object();

        public ThreadSafeList()
        {
            unsafeList = new List<T>();
        }

        public void Add(T element)
        {
            lock (monitor)
            {
                if (!unsafeList.Contains(element))
                    unsafeList.Add(element);
            }
        }

        public T[] GetElements()
        {
            lock (monitor)
            {
                var safeList = unsafeList.ToArray();
                Array.Sort(safeList);
                return safeList;
            }
        }
    }
}
