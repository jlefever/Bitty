using System.Collections.Generic;

namespace Bitty
{
    public abstract class BNode { }

    public class BDict : BNode
    {
        IDictionary<byte[], BNode> Value { get; }

        public BDict(IDictionary<byte[], BNode> value)
        {
            Value = value;
        }
    }

    public class BList : BNode
    {
        IList<BNode> Value { get; }

        public BList(IList<BNode> value)
        {
            Value = value;
        }
    }

    public class BInt : BNode
    {
        public long Value { get; }

        public BInt(long value)
        {
            Value = value;
        }
    }

    public class BString : BNode
    {
        public byte[] Value { get; }

        public BString(byte[] value)
        {
            Value = value;
        }
    }
}
