using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace EscapeRoute.Abstractions.DataStructures
{
    // Adapted from System.Collections.Generic.ValueListBuilder,
    // and System.Runtime.CompilerServices.DefaultInterpolatedStringHandler
    /// <summary>
    /// A fast and efficient store for the values that make up a new string created by the <see cref="TokenReplacementEngine"/>.
    /// </summary>
    public ref struct StringValueList
    {
        // A span to give us some fast access to the memory underneath _array.
        private Span<char> _span;
        
        // Backing array for the string.
        private char[] _array;
        
        // Tracks the current length of the string withing the _span.
        private int _pos;
        
        // We are assuming here that we want a reasonably sized result string,
        // so reduce Grow operations by making the default large. TODO: Tune as necessary.
        private const int _minimumCapacity = 1024;
        
        // A bit lower than the theoretical limit of 2^30ish as there's no clear information on what the *real* limit is.
        private const int _maxCapacity = 1_000_000_000;

        /// <summary>
        /// Initialise a <see cref="StringValueList"/> with the specified capacity.
        /// </summary>
        /// <param name="initialCapacity">The desired minimum capacity of the <see cref="StringValueList"/></param>
        public StringValueList(int initialCapacity)
        {
            var capacityToRent = GetInitialLength(initialCapacity);
            _array = ArrayPool<char>.Shared.Rent(capacityToRent);
            _span = _array;
            _pos = 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(ReadOnlySpan<char> value)
        {
            if (value.Length + _pos > _span.Length)
            {
                Grow(value.Length);
            }

            value.CopyTo(_span.Slice(_pos, value.Length));
            
            _pos += value.Length;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            var s = _span.Slice(0, _pos).ToString();
            Dispose(); 
            return s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetInitialLength(int initialLength)
        {
            var size = Math.Min(initialLength, _maxCapacity);
            size = Clamp(size, _minimumCapacity, _maxCapacity);
            return size;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetNewArraySize(int additionLength)
        {
            var size = Math.Max((_pos + additionLength), Math.Min((_array.Length * 2), _maxCapacity));
            size = Clamp(size, _minimumCapacity, _maxCapacity);
            return size;
        }
        
        private void Grow(int additionLength)
        {
            var newSize = GetNewArraySize(additionLength);
            
            var newArray = ArrayPool<char>.Shared.Rent(newSize);

            _span.Slice(0, _pos).CopyTo(newArray);

            if (_array != null)
            {
                ArrayPool<char>.Shared.Return(_array);
            }
            
            _span = _array = newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Dispose()
        {
            var array = _array;
            this = default; // Reset this instance, we are done with it, any new adds will be to a new StringValueList.
            if (array != null)
            {
                ArrayPool<char>.Shared.Return(array);
            }
        }
        
        /// <summary>
        /// Borrowed from System.Math implementation in .NET Standard 2.1 and greater.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            
            return value > max 
                   ? max 
                   : value;
        }
    }
}