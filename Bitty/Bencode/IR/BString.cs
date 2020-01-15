using Bitty.Bencode.IR;

namespace Bitty
{
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
