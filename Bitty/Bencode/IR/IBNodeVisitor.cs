namespace Bitty.Bencode.IR
{
    public interface IBNodeVisitor<out TResult>
    {
        TResult VisitBDict(BDict node);
        TResult VisitBList(BList node);
        TResult VisitBInt(BInt node);
        TResult VisitBString(BString node);
    }
}
