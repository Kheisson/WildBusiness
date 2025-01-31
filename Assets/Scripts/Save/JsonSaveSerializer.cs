using Newtonsoft.Json;

namespace Save
{
    public class JsonSaveSerializer : ISaveSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T Deserialize<T>(string serializedData)
        {
            return JsonConvert.DeserializeObject<T>(serializedData);
        }
    }
}