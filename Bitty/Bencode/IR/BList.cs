using System.Collections.Generic;

namespace Bitty.Bencode.IR
{
    public class BList : BNode
    {
        public IList<BNode> Value { get; }

        public BList()
        {
            Value = new List<BNode>();
        }

        public BList(IList<BNode> value)
        {
            Value = value;
        }

        public override TResult Accept<TResult>(IBNodeVisitor<TResult> visitor)
        {
            return visitor.VisitBList(this);
        }
    }
}
