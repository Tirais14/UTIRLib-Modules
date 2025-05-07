#nullable enable
using System.Collections;
using System.Collections.Generic;

namespace UTIRLib
{
    public struct ArrayEnumerator<T> : IEnumerator<T?>
    {
        private readonly T?[] array;
        private int index;

        public readonly T? Current => array[index];
        readonly object? IEnumerator.Current => Current;

        public ArrayEnumerator(T?[] array)
        {
            this.array = array;
            index = 0;
        }

        public bool MoveNext() => ++index < array.Length;

        public void Reset() => index = 0;

        public readonly void Dispose() { }
    }
}
