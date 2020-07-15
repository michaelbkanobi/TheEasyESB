using System;

namespace TheEasyEsb
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Text { get; set; }
    }
    public class MessageReceived
    {
        public Guid MessageId { get; set; }
    }
}
