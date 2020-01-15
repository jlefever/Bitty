using System.Collections.Generic;

namespace Bitty.Bencode.IR
{
    public class BDict : BNode
    {
        public IDictionary<BString, BNode> Value { get; }

        public BDict(IDictionary<BString, BNode> value)
        {
            Value = value;
        }

        public override TResult Accept<TResult>(IBNodeVisitor<TResult> visitor)
        {
            return visitor.VisitBDict(this);
        }
    }
}
