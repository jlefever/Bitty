using System;
using System.Collections.Generic;

namespace Bitty.Bencode.IR
{
    public class BDict : BNode
    {
        public SortedDictionary<BString, BNode> Value { get; }

        public BDict()
        {
            Value = new SortedDictionary<BString, BNode>(new LexicalComparer());
        }

        public BDict(IDictionary<BString, BNode> value)
        {
            Value = new SortedDictionary<BString, BNode>(value, new LexicalComparer());
        }

        public override TResult Accept<TResult>(IBNodeVisitor<TResult> visitor)
        {
            return visitor.VisitBDict(this);
        }

        private class LexicalComparer : Comparer<BString>
        {
            // This is a modified Comparer from
            // https://stackoverflow.com/questions/5108091/java-comparator-for-byte-array-lexicographic
            // null is always the smallest value
            public override int Compare(BString left, BString right)
            {
                if (ReferenceEquals(left, right))
                {
                    return 0;
                }

                if (left is null)
                {
                    return -1;
                }

                if (right is null)
                {
                    return 1;
                }

                var length = Math.Min(left.Value.Length, right.Value.Length);

                for (int i = 0; i < length; i++)
                {
                    if (left.Value[i] != right.Value[i])
                    {
                        return left.Value[i] - right.Value[i];
                    }
                }

                return left.Value.Length - right.Value.Length;
            }
        }
    }
}
