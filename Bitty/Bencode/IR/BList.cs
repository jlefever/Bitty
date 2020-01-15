namespace Bitty.Bencode.IR
{
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
}
