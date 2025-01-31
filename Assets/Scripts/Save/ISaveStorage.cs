namespace Save
{
    public interface ISaveStorage
    {
        void Save(string key, string value);
        string Load(string key);
        bool Exists(string key);
    }
}