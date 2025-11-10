using DoAnTinHoc.Data;
using DoAnTinHoc.Models;

namespace DoAnTinHoc.DSA
{
    public class EmployeeRepository
    {
        private readonly JsonFileStorage<Employee> _storage;
        private List<Employee> _items;
        private readonly object _lock = new object();

        public EmployeeRepository(string filePath)
        {
            _storage = new JsonFileStorage<Employee>(filePath);
            _items = _storage.Load();
        }

        public List<Employee> GetAll() => _items.OrderBy(x => x.Id).ToList();
        public Employee? GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void AddOrUpdate(Employee e)
        {
            lock (_lock)
            {
                var ex = _items.FirstOrDefault(x => x.Id == e.Id);
                if (ex == null)
                {
                    var next = _items.Any() ? _items.Max(x => x.Id) + 1 : 1;
                    e.Id = next;
                    _items.Add(e);
                }
                else
                {
                    ex.Name = e.Name;
                    ex.Position = e.Position;
                    ex.Phone = e.Phone;
                    ex.Email = e.Email;
                    ex.Active = e.Active;
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
