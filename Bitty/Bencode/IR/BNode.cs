namespace Bitty.Bencode.IR
{
    public abstract class BNode
    {
        public abstract TResult Accept<TResult>(IBNodeVisitor<TResult> visitor);
    }
}
