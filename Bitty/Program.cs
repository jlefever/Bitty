using System.IO;

namespace Bitty
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = @"C:\Users\Jason\Downloads\ubuntu-19.10-desktop-amd64.iso.torrent";

            var bytes = File.ReadAllBytes(filename);

            var parser = new BParser(bytes);

            var root = parser.Parse();
        }
    }
}
