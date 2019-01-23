using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
    public struct DsnCardinality : IEquatable<DsnCardinality>
    {
        /// <summary>
        /// Initializes a new <see cref="DsnCardinality"/>.
        /// </summary>
        /// <param name="min">Minimal cardinality. Must be equal or greater than 0.</param>
        /// <param name="max">Maximal cardinality: 0 for * (no limit) or a positive number.</param>
        public DsnCardinality( int min, int max = 0 )
        {
            if (min < 0) throw new ArgumentException("Min must be zero or positive.", nameof(min));
            if (max < 0) throw new ArgumentException("Max must be zero (max cardinality) or positive.", nameof(max));
            if (min > max && max != 0) throw new ArgumentException("Max must be greater than Min.");
            Min = min;
            Max = max;
        }

        public int Min { get; }

        public int Max { get; }

        public bool Equals(DsnCardinality other) => Min == other.Min && Max == other.Max;

        public override bool Equals(object obj) => obj is DsnCardinality c ? Equals(c) : false;

        public override int GetHashCode() => Min + Max;

        public override string ToString() => $"({Min},{(Max == 0 ? "*" : Max.ToString())})";
    }
}
