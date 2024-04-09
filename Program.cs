using System.Collections;
using System.Linq.Expressions;

var array = new int[] { 1, 2, 3,};
var element = new Element<int>(array);

public class Element<T> : IEnumerable<T>
{
    private T[] values;
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