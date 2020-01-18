using Bitty.Bencode.Serialize;
using System.Collections.Generic;

namespace Bitty
{
    public class TorrentInfo
    {
        [BKeyRequired]
        [BKeyName("name")]
        public string Name { get; set; }

        [BKeyRequired]
        [BKeyName("piece length")]
        public long PieceLength { get; set; }

        [BKeyRequired]
        [BKeyName("pieces")]
        public string Pieces { get; set; }

        [BKeyName("private")]
        public long Private { get; set; }

        [BKeyName("length")]
        public long Length { get; set; }

        [BKeyName("files")]
        public IList<TorrentFile> Files { get; set; }
    }
}
