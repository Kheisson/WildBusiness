namespace Save
{
    public interface ISaveSerializer
    {
        public string Serialize<T>(T obj);
        public T Deserialize<T>(string json);
    }
}