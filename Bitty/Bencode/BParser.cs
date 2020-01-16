using Bitty.Bencode.IR;
using System;
using System.Collections.Generic;

namespace Bitty.Bencode
{
    public class BParser
    {
        private class BParserException : Exception
        {
            public BParserException(string message) : base(message) { }
        }

        private readonly BParserErrorListener _errorListener;
        private readonly byte[] _source;
        private int _current = 0;

        public BParser(byte[] source, BParserErrorListener errorListener)
        {
            _source = source;
            _errorListener = errorListener;
        }

        public BNode Parse()
        {
            try
            {
                return ParseAnyType();
            }
            catch (BParserException e)
            {
                _errorListener.Fail(_current, e.Message);
                return null;
            }
        }

        private BNode ParseAnyType()
        {
            var b = (char)Peek();

            switch (b)
            {
                case BLiteral.StartDict:
                    Advance();
                    return ParseDict();
                case BLiteral.StartList:
                    Advance();
                    return ParseList();
                case BLiteral.StartInt:
                    Advance();
                    return ParseInt();
                default:
                    return ParseString();
            }
        }

        private BDict ParseDict()
        {
            // TODO: Ensure uniqueness and order of keys
            var dict = new Dictionary<BString, BNode>();

            do
            {
                var key = ParseString();
                var value = ParseAnyType();
                dict.TryAdd(key, value);
            }
            while (!IsAtEnd() && !Match(BLiteral.End));

            return new BDict(dict);
        }

        private BList ParseList()
        {
            var list = new List<BNode>();

            do
            {
                list.Add(ParseAnyType());
            }
            while (!IsAtEnd() && !Match(BLiteral.End));

            return new BList(list.ToArray());
        }

        private BInt ParseInt()
        {
            var value = ParseSignedNumber();
            Consume(BLiteral.End, "Expected '"
                + BLiteral.End + "' to terminate integer.");
            return new BInt(value);
        }

        private BString ParseString()
        {
            var length = ParseNumber();
            Consume(BLiteral.StrDelimitor, "Expcted '"
                + BLiteral.StrDelimitor + "' after length of string.");

            var bytes = new byte[length];

            for (int i = 0; i < length; i++)
            {
                bytes[i] = Advance();
            }

            return new BString(bytes);
        }

        private long ParseSignedNumber()
        {
            var hasMinus = Match(BLiteral.Minus);
            var value = ParseNumber();

            if (value == 0 && hasMinus)
            {
                throw new BParserException("'-0' is not a valid integer.");
            }

            return hasMinus ? -1L * value : value;
        }

        private long ParseNumber()
        {
            var digits = new List<char>();

            do
            {
                digits.Add((char)Advance());
            }
            while (!IsAtEnd() && IsDigit(Peek()));

            var text = string.Concat(digits);

            if (!long.TryParse(text, out long number))
            {
                throw new BParserException("Could not parse number as Int64.");
            }

            return number;
        }

        private void Consume(char c, string message)
        {
            Consume((byte)c, message);
        }

        private void Consume(byte b, string message)
        {
            if (!Match(b))
            {
                throw new BParserException(message);
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
