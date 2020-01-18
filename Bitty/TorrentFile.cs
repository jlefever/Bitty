using Bitty.Bencode.Serialize;
using System.Collections.Generic;

namespace Bitty
{
    public class TorrentFile
    {
        [BKeyRequired]
        [BKeyName("length")]
        public long Length { get; set; }

        [BKeyRequired]
        [BKeyName("path")]
        public IList<string> Path { get; set; }
    }
}
