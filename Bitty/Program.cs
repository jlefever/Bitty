using Bitty.Bencode;
using System;
using System.IO;

namespace Bitty
{
    public class Program
    {
        static void Main(string[] args)
        {
            var readFile = @"C:\Users\Jason\Downloads\ubuntu-19.10-desktop-amd64.iso.torrent";

            var writeFile = @"C:\Users\Jason\Downloads\ubuntu-19.10-desktop-amd64.iso.MINE.torrent";

            var bytes = File.ReadAllBytes(readFile);

            var parser = new BParser(bytes, new BParserErrorListener(Console.Out));

            var root = parser.Parse();

            var printer = new BPrinter();

            root.Accept(printer);

            File.WriteAllBytes(writeFile, printer.GetBytes());

            var didItWork = CompareFiles(readFile, writeFile);
        }

        private static bool CompareFiles(string a, string b)
        {
            return CompareByteArrays(File.ReadAllBytes(a), File.ReadAllBytes(b));
        }

        private static bool CompareByteArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for(var i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
