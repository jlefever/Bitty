using System;
using System.Collections.Generic;

namespace Bitty
{
    public class BParser
    {
        private readonly byte[] _source;
        private int _start = 0;
        private int _current = 0;

        public BParser(byte[] source)
        {
            _source = source;
        }

        public BNode Parse()
        {
            return ParseAnyType();
        }

        private BNode ParseAnyType()
        {
            var b = (char)Peek();

            switch (b)
            {
                case 'd': Advance(); return ParseDict();
                case 'l': Advance(); return ParseList();
                case 'i': Advance(); return ParseInt();
                default: return ParseString();
            }
        }

        private BDict ParseDict()
        {
            // TODO: Ensure uniqueness and order of keys
            var dict = new Dictionary<byte[], BNode>();

            do
            {
                var key = ParseString();
                var value = ParseAnyType();
                dict.TryAdd(key.Value, value);
            }
            while (!IsAtEnd() && !Match('e'));

            return new BDict(dict);
        }

        private BList ParseList()
        {
            var list = new List<BNode>();

            do
            {
                list.Add(ParseAnyType());
            }
            while (!IsAtEnd() && !Match('e'));

            return new BList(list);
        }

        private BInt ParseInt()
        {
            var value = ParseSignedNumber();
            Consume('e', "Expected 'e' to terminate integer");
            return new BInt(value);
        }

        private BString ParseString()
        {
            var length = ParseNumber();
            Consume(':', "Expcted ':' after length of string");

            // TODO: Use Extract() !!
            var bytes = new List<byte>();

            for(int i = 0; i < length; i++)
            {
                bytes.Add(Advance());
            }

            return new BString(bytes.ToArray());
        }

        private long ParseSignedNumber()
        {
            var hasMinus = Match('-');
            var value = ParseNumber();
            return hasMinus ? -1L * value : value;
        }

        private long ParseNumber()
        {
            // TODO: Use Extract() !!
            var digits = new List<char>();

            do
            {
                digits.Add((char)Advance());
            }
            while (!IsAtEnd() && IsDigit(Peek()));

            return Convert.ToInt64(string.Concat(digits));
        }

        private void Consume(char c, string message)
        {
            Consume((byte)c, message);
        }

        private void Consume(byte b, string message)
        {
            if (!Match(b))
            {
                // TODO: Proper error reporting
                throw new Exception(message);
            }
        }

        private bool Match(char expected)
        {
            return Match((byte)expected);
        }

        private bool Match(byte expected)
        {
            if (IsAtEnd() || _source[_current] != expected)
            {
                return false;
            }

            _current++;
            return true;
        }

        private byte Peek()
        {
            return IsAtEnd() ? (byte)0 : _source[_current];
        }

        private static bool IsDigit(byte b)
        {
            return b >= '0' && b <= '9';
        }

        private static bool IsAscii(byte c)
        {
            return c < 128;
        }

        private bool IsAtEnd()
        {
            return _current >= _source.Length;
        }

        private byte Advance()
        {
            _current++;
            return _source[_current - 1];
        }
    }
}
