using System;

namespace Bitty.Bencode.Serialize
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BKeyNameAttribute : Attribute
    {
        public string Key { get; }

        public BKeyNameAttribute(string key)
        {
            Key = key;
        }
    }
}
