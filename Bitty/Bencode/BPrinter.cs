using Bitty.Bencode.IR;
using System.Collections.Generic;
using System.Text;

namespace Bitty.Bencode
{
    public class BPrinter : IBNodeVisitor<object>
    {
        private readonly List<byte> _bytes;

        public BPrinter()
        {
            _bytes = new List<byte>();
        }

        public void Clear()
        {
            _bytes.Clear();
        }

        public byte[] GetBytes()
        {
            return _bytes.ToArray();
        }

        public object VisitBDict(BDict node)
        {
            _bytes.Add((byte)BLiteral.StartDict);

            foreach (var pair in node.Value)
            {
                pair.Key.Accept(this);
                pair.Value.Accept(this);
            }

            _bytes.Add((byte)BLiteral.End);
            return null;
        }

        public object VisitBInt(BInt node)
        {
            _bytes.Add((byte)BLiteral.StartInt);
            _bytes.AddRange(ToAsciiBytes(node.Value));
            _bytes.Add((byte)BLiteral.End);
            return null;
        }

        public object VisitBList(BList node)
        {
            _bytes.Add((byte)BLiteral.StartList);

            foreach (var item in node.Value)
            {
                item.Accept(this);
            }

            _bytes.Add((byte)BLiteral.End);
            return null;
        }

        public object VisitBString(BString node)
        {
            _bytes.AddRange(ToAsciiBytes(node.Value.LongLength));
            _bytes.Add((byte)BLiteral.StrDelimitor);

            foreach (var b in node.Value)
            {
                _bytes.Add(b);
            }

            return null;
        }

        private byte[] ToAsciiBytes(long value)
        {
            return Encoding.ASCII.GetBytes(value.ToString());
        }
    }
}
