using System.IO;

namespace Save
{
    public class FileSaveStorage : ISaveStorage
    {
        private readonly string _savePath;
        
        public FileSaveStorage(string savePath)
        {
            _savePath = savePath;
            
            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }
        }
        
        public void Save(string key, string value)
        {
            var filePath = Path.Combine(_savePath, $"{key}.sav");
            File.WriteAllText(filePath, value);
        }
        
        public string Load(string key)
        {
            var filePath = Path.Combine(_savePath, $"{key}.sav");
            return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
        }

        public bool Exists(string key)
        {
            var filePath = Path.Combine(_savePath, $"{key}.sav");
            return File.Exists(filePath);
        }
    }
}