namespace Bitty.Bencode.IR
{
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
}
