using System.Collections;
using System.Collections.Generic;

namespace MegaScrypt
{
    public class Array : Object, IEnumerable<object>
    {
        private List<object> _elements;

        public IEnumerator<object> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public List<object> ToList() => new List<object>(_elements);

        public object this[int i]
        {
            get => _elements[i];
            set => _elements[i] = value;
        }

        public int Count => _elements.Count;

        public Array()
        {
            Bind();
            _elements = new List<object>();
        }

        public Array(IEnumerable<object> list)
        {
            Bind();
            _elements = new List<object>(list);
        }

        private void Bind()
        {
            Declare(Add);
            Declare(AddRange);
            Declare(Insert);
            Declare(RemoveAt);
            Declare(Clear);
            Declare("Count", () => _elements.Count);
        }

        private object Add(List<object> parameters)
        {
            _elements.Add(parameters[0]);
            return null;
        }

        public void Add(object o) => _elements.Add(o);

        private object AddRange(List<object> parameters)
        {
            _elements.AddRange(parameters[0] as IEnumerable<object>);
            return null;
        }

        public void AddRange(IEnumerable<object> objs) => _elements.AddRange(objs);

        private object Insert(List<object> parameters)
        {
            _elements.Insert((int)parameters[0], parameters[1]);
            return null;
        }

        public void Insert(int index, object o) => _elements.Insert(index, o);

        private object RemoveAt(List<object> parameters)
        {
            _elements.RemoveAt((int)parameters[0]);
            return null;
        }

        public void RemoveAt(int index) => _elements.RemoveAt(index);

        private object Clear(List<object> parameters)
        {
            _elements.Clear();
            return null;
        }

        public void Clear() => _elements.Clear();
    }
}
