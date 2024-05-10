using System.Collections;
using System.Linq.Expressions;


namespace CustomProvider
{
    public class Element<T> : IQueryable<T>
    {
        private T[] values;

        public Type ElementType
        {
            get { return typeof(T); }
        }
        public Expression Expression => throw new NotImplementedException();
        public IQueryProvider Provider => throw new NotImplementedException();
        public Element(T[] list)
        {
            values = new T[list.Length];
            for(int i = 0; i < values.Length; i++)
            {
                this.values[i] = list[i];
            }
        } 
        public struct ElementEnumerator : IEnumerator<T>
        {
            public T[] values;
            private int position = -1;
            object IEnumerator.Current => Current ?? throw new ArgumentException(nameof(Current));
            public T Current => values[position];

            public ElementEnumerator(T[] values)
            {
                this.values = values;
            }
            public bool MoveNext()
            {
                position++;
                return (position < values.Length);
            }

            public void Dispose() => GC.SuppressFinalize(this);
            public void Reset() => position = -1;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new ElementEnumerator(values);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}