using System;

namespace Chatter
{
    public class Message
    {
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $"[{TimeStamp.ToLongTimeString()}] {Name}: {Content}";
        }
    }
}
