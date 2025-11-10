using DoAnTinHoc.Models;
using System.Linq;

namespace DoAnTinHoc.Data
{
    public class CategoryRepository
    {
        private readonly JsonFileStorage<Category> _storage;
        private List<Category> _items;
        private readonly object _lock = new object();

        public CategoryRepository(string filePath)
        {
            _storage = new JsonFileStorage<Category>(filePath);
            _items = _storage.Load();
        }

        public CategoryRepository()
        {
        }

        public List<Category> GetAll() => _items.OrderBy(x => x.Id).ToList();
        public Category? GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void AddOrUpdate(Category c)
        {
            lock (_lock)
            {
                var ex = _items.FirstOrDefault(x => x.Id == c.Id);
                if (ex == null)
                {
                    var next = _items.Any() ? _items.Max(x => x.Id) + 1 : 1;
                    c.Id = next;
                    _items.Add(c);
                }
                else
                {
                    ex.Name = c.Name;
                }
                Persist();
            }
        }

        public void DeleteById(int id)
        {
            lock (_lock)
            {
                _items.RemoveAll(x => x.Id == id);
                Persist();
            }
        }

        private void Persist() => _storage.Save(_items);
    }
}
