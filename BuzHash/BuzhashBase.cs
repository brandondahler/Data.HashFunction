using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public abstract class BuzHashBase
        : HashFunctionBase
    {
        /// <summary>
        /// Random table of 256 UInt64s.  
        /// </summary>
        public abstract UInt64[] Rtab { get; }

        public abstract CShiftDirection ShiftDirection { get; }

        public virtual UInt64 InitVal { get { return 0; } }

        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 8, 16, 32, 64 }; }
        }

        public enum CShiftDirection
        {
            Left,
            Right
        }


        protected BuzHashBase(int defaultHashSize = 64)
            : base(defaultHashSize)
        {

        }

        public override byte[] ComputeHash(byte[] data)
        {
            switch (HashSize)
            {
                case 8:
                    return ComputeHash8(data);

                case 16:
                    return ComputeHash16(data);

                case 32:
                    return ComputeHash32(data);
                
                case 64:
                    return ComputeHash64(data);

                default:
                    throw new ArgumentOutOfRangeException("HashSize is set to an invalid value.");
            }
        }


        protected byte[] ComputeHash8(byte[] data)
        {
            byte h = (byte) InitVal;

            foreach (byte b in data)
                h = (byte) (CShift(h, 1) ^ (UInt32) Rtab[(int) b]);

            return BitConverter.GetBytes(h);
        }

        protected byte[] ComputeHash16(byte[] data)
        {
            UInt16 h = (UInt16) InitVal;

            foreach (byte b in data)
                h = (UInt16) (CShift(h, 1) ^ (UInt32) Rtab[(int)b]);

            return BitConverter.GetBytes(h);
        }

        protected byte[] ComputeHash32(byte[] data)
        {
            UInt32 h = (UInt32) InitVal;

            foreach (byte b in data)
                h = CShift(h, 1) ^ (UInt32) Rtab[(int) b];
            
            return BitConverter.GetBytes(h);
        }

        protected byte[] ComputeHash64(byte[] data)
        {
            UInt64 h = InitVal;

            foreach (byte b in data)
                h = CShift(h, 1) ^ Rtab[(int)b];
            
            return BitConverter.GetBytes(h);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected byte CShift(byte n, int shiftBy)
        {
            switch (ShiftDirection)
            {
                default:
                case CShiftDirection.Left:
                    return (byte) ((n << shiftBy) | (n >> (8 - shiftBy)));

                case CShiftDirection.Right:
                    return (byte) ((n >> shiftBy) | (n << (8 - shiftBy)));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt16 CShift(UInt16 n, int shiftBy)
        {
            switch (ShiftDirection)
            {
                default:
                case CShiftDirection.Left:
                    return (UInt16) ((n << shiftBy) | (n >> (16 - shiftBy)));

                case CShiftDirection.Right:
                    return (UInt16) ((n >> shiftBy) | (n << (16 - shiftBy)));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt32 CShift(UInt32 n, int shiftBy)
        {
            switch (ShiftDirection)
            {
                default:
                case CShiftDirection.Left:
                    return (n << shiftBy) | (n >> (32 - shiftBy));

                case CShiftDirection.Right:
                    return (n >> shiftBy) | (n << (32 - shiftBy));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt64 CShift(UInt64 n, int shiftBy)
        {
            switch (ShiftDirection)
            {
                default:
                case CShiftDirection.Left:
                    return (n << shiftBy) | (n >> (64 - shiftBy));

                case CShiftDirection.Right:
                    return (n >> shiftBy) | (n << (64 - shiftBy));
            }
        }

    }
}
