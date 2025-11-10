using System.Text.Json;

namespace DoAnTinHoc.Data
{
    public class JsonFileStorage<T>
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _opts = new JsonSerializerOptions { WriteIndented = true };

        public JsonFileStorage(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public List<T> Load()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _opts) ?? new List<T>();
        }

        public void Save(List<T> items)
        {
            var json = JsonSerializer.Serialize(items, _opts);
            var tmp = _filePath + ".tmp";
            File.WriteAllText(tmp, json);
            File.Copy(tmp, _filePath, true);
            File.Delete(tmp);

        }
    }
}
