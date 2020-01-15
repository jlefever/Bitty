using System.IO;

namespace Bitty.Bencode
{
    public class BParserErrorListener
    {
        private readonly TextWriter _writer;

        public BParserErrorListener(TextWriter writer)
        {
            _writer = writer;
        }

        public void Fail(int index, string message)
        {
            _writer.WriteLine($"Failed at index {index}: {message}");
        }
    }
}
