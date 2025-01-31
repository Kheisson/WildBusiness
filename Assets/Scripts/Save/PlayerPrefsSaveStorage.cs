using UnityEngine;

namespace Save
{
    public class PlayerPrefsSaveStorage : ISaveStorage
    {
        public void Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string Load(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public bool Exists(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}