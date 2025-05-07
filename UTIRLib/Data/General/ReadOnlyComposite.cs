using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace UTIRLib   
{
    public class ReadOnlyComposite<T> : IReadOnlyComposite<T>
    {
        protected readonly T[] childs = null!;
        protected int childsCount;

        public int Count => childsCount;
        public T this[int index] => childs[index];

        public ReadOnlyComposite() 
        { }
        public ReadOnlyComposite(T[] childs)
        {
            this.childs = childs;
            childsCount = childs.Length;
        }

        public T GetChild(int index) => childs[index];

        public bool Contains(object? value) => Contains(value.ConvertToType<T>());
        public virtual bool Contains(T? value) => value.IsNotDefault() && Array.IndexOf(childs, value) >= 0;

        public IEnumerator<T> GetEnumerator() => childs.GetEnumeratorT();
        IEnumerator IEnumerable.GetEnumerator() => childs.GetEnumerator();

        object IReadOnlyComposite.GetChild(int index) => GetChild(index)!;
    }
}
