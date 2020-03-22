using System;

namespace EssenceOfMagic
{
    public class MessagesQueue
    {
        private Message[] Queue = new Message[0];
        public int Count { get; private set; } = 0;
        public void Add(Message message)
        {
            Count++;
            Array.Resize<Message>(ref Queue, Count);
            Queue[Count - 1] = message;
        }
        public Message Get()
        {
            return Queue[0];
        }
        public Message GetNext()
        {
            Count--;
            Message[] newqueue = new Message[Count];
            for (int i = 0; i < Count; i++) newqueue[i] = Queue[i + 1];
            Queue = newqueue;
            return Queue[0];
        }
    }
    public class Message
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public MessageType Type { get; set; }
    }
    public enum MessageType
    {
        Hint,
        Dialog,
        Answer
    }
}
