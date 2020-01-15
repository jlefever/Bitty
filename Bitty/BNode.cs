using System.Collections.Generic;

namespace Bitty
{
    public interface IBNodeVisitor<out TResult>
    {
        TResult VisitBDict(BDict node);
        TResult VisitBList(BList node);
        TResult VisitBInt(BInt node);
        TResult VisitBString(BString node);
    }

    public abstract class BNode
    {
        public abstract TResult Accept<TResult>(IBNodeVisitor<TResult> visitor);
    }

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

    public class BList : BNode
    {
        public BNode[] Value { get; }

        public BList(BNode[] value)
        {
            Value = value;
        }

        public override TResult Accept<TResult>(IBNodeVisitor<TResult> visitor)
        {
            return visitor.VisitBList(this);
        }
    }

    public class BInt : BNode
    {
        public long Value { get; }

        public BInt(long value)
        {
            Value = value;
        }

        public override TResult Accept<TResult>(IBNodeVisitor<TResult> visitor)
        {
            return visitor.VisitBInt(this);
        }
    }

    public class BString : BNode
    {
        public byte[] Value { get; }

        public BString(byte[] value)
        {
            Value = value;
        }

        public override TResult Accept<TResult>(IBNodeVisitor<TResult> visitor)
        {
            return visitor.VisitBString(this);
        }
    }
}
