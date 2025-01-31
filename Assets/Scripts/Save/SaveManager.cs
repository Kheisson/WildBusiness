using Infra;

namespace Save
{
    public class SaveManager
    {
        private readonly ISaveSerializer _serializer;
        private readonly ISaveStorage _storage;
        
        public SaveManager(ISaveSerializer serializer, ISaveStorage storage)
        {
            _serializer = serializer;
            _storage = storage;
        }
        
        public void Save<T>(string key, T obj)
        {
            var json = _serializer.Serialize(obj);
            _storage.Save(key, json);
            
            LlamaLog.LogInfo($"Saved {key}.");
        }
        
        public T Load<T>(string key)
        {
            if (!_storage.Exists(key))
            {
                LlamaLog.LogWarning($"Save file {key} does not exist.");
                return default;
            }
            
            var json = _storage.Load(key);
            
            LlamaLog.LogInfo($"Loaded {key}.");
            
            return _serializer.Deserialize<T>(json);
        }
        
        public bool Exists(string key)
        {
            LlamaLog.LogInfo($"Checking if {key} exists.");
            
            return _storage.Exists(key);
        }
    }
}