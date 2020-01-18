using Bitty.Bencode.Serialize;
using System.Collections.Generic;

namespace Bitty
{
    public class Torrent
    {
        [BKeyRequired]
        [BKeyName("announce")]
        public string Announce { get; set; }

        [BKeyName("announce-list")]
        public IList<string> AnnounceList { get; set; }

        [BKeyName("creation date")]
        public long CreationDate { get; set; }

        [BKeyName("comment")]
        public string Comment { get; set; }

        [BKeyName("created by")]
        public string CreatedBy { get; set; }

        [BKeyName("encoding")]
        public string Encoding { get; set; }
    }
}
