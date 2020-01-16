using System;
using System.Collections.Generic;

namespace Bitty.Bencode.IR
{
    public class BStringComparer : Comparer<BString>
    {
        // This is a modified Comparer from
        // https://stackoverflow.com/questions/5108091/java-comparator-for-byte-array-lexicographic
        public override int Compare(BString x, BString y)
        {
            var left = x.Value;
            var right = y.Value;

            var length = Math.Min(left.Length, right.Length);

            for (int i = 0; i < length; i++)
            {
                var a = left[i] & 0xff;
                var b = right[i] & 0xff;

                if (a != b)
                {
                    return a - b;
                }
            }

            return left.Length - right.Length;
        }
    }
}
