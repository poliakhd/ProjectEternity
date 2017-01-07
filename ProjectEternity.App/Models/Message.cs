namespace ProjectEternity.App.Models
{
    public class Message<T>
    {
        public string Tag { get; set; }
        public T Value { get; set; }

        public Message(T value) 
            : this(value, null)
        {
            
        }

        public Message(T value, string tag)
        {
            Value = value;
            Tag = tag;
        }
    }
}