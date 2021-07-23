using System;
using System.Buffers;

namespace EscapeRoute.DataStructures
{
    /// <summary>
    /// Adapted from System.Collections.Generic.ValueListBuilder
    /// </summary>
    internal ref struct ValueList<T> where T : struct
    {
        private Span<T> _span;
        private T[] _array;
        private int _length;

        public ValueList(Span<T> span)
        {
            _span = span;
            _array = null;
            _length = 0;
        }

        public int Length => _length;

        public ref T this[int index] => ref _span[index];

        public void Add(T item)
        {
            var length = _length;
            if (length >= _span.Length)
            {
                Grow();
            }

            _span[length] = item;
            _length = (length + 1);
        }

        public void AddRange(ReadOnlySpan<T> span)
        {
            foreach (var item in span)
            {
                Add(item);
            }
        }

        public bool IsEmpty => _length == 0;

        public ReadOnlySpan<T> AsSpan()
        {
            return _span.Slice(0, _length);
        }

        public ReadOnlyMemory<T> AsMemory()
        {
            return new ReadOnlyMemory<T>(_array, 0, _length);
        }

        private void Grow()
        {
            var length = _span.Length;
            
            if (_span.IsEmpty)
            {
                length = 1;
            }

            var array = ArrayPool<T>.Shared.Rent(length * 2);

            _span.TryCopyTo(array);

            var arrayToReturn = _array;
            _array = array;
            _span = _array;

            if (arrayToReturn != null)
            {
                ArrayPool<T>.Shared.Return(_array);
            }
        }
    }
}