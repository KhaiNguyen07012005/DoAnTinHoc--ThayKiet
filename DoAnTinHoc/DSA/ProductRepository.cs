using DoAnTinHoc.Models;
using DoAnTinHoc.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnTinHoc.Data
{

    public class ProductRepository
    {
        private readonly JsonFileStorage<Product> _storage;
        private BinarySearchTree _bst = new BinarySearchTree();
        private static readonly object _fileLock = new object();
        private readonly CategoryRepository _catRepo;
        public ProductRepository(string filePath, CategoryRepository catRepo)
        {
            _storage = new JsonFileStorage<Product>(filePath);
            _catRepo = catRepo ?? new CategoryRepository(); 
            var all = _storage.Load() ?? new List<Product>();
            foreach (var p in all) _bst.Insert(p);
        }

     
        public ProductRepository()
            : this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.json"), new CategoryRepository())
        {
        }
        public List<Product> GetAll() => _bst.InOrder();


        public List<Product> Load() => _storage.Load();
        public void Save(List<Product> products)
        {
            lock (_fileLock)
            {
                _bst = new BinarySearchTree();
                foreach (var p in products) _bst.Insert(p);
                _storage.Save(_bst.InOrder());
            }
        }
        public void SaveAll(List<Product> products) => Save(products);


        public Product? GetById(int id) => _bst.Search(id);
        public void AddOrUpdate(Product p)
        {
            lock (_fileLock)
            {
                if (p.Id == 0)
                {
                    var list = _bst.InOrder();
                    p.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
                    if (p.CreatedDate == default) p.CreatedDate = DateTime.Now;
                }
                else if (p.CreatedDate == default)
                {
                    p.CreatedDate = DateTime.Now;
                }

                _bst.Insert(p);
                _storage.Save(_bst.InOrder());
            }
        }

        public void DeleteById(int id)
        {
            lock (_fileLock)
            {
                var list = _bst.InOrder();
                list.RemoveAll(x => x.Id == id);
                _bst = new BinarySearchTree();
                foreach (var p in list) _bst.Insert(p);
                _storage.Save(_bst.InOrder());
            }
        }

        public void IncreaseStock(int productId, int delta)
        {
            if (delta <= 0) throw new ArgumentException("delta phải > 0");
            lock (_fileLock)
            {
                var p = GetById(productId);
                if (p == null) throw new KeyNotFoundException("Sản phẩm không tồn tại");
                p.Quantity += delta;
                _bst.Insert(p);
                _storage.Save(_bst.InOrder());
            }
        }

        public void DecreaseStock(int productId, int delta)
        {
            if (delta <= 0) throw new ArgumentException("delta phải > 0");
            lock (_fileLock)
            {
                var p = GetById(productId);
                if (p == null) throw new KeyNotFoundException("Sản phẩm không tồn tại");
                if (p.Quantity < delta) throw new InvalidOperationException($"Kho không đủ: {p.Name} (tồn {p.Quantity})");
                p.Quantity -= delta;
                _bst.Insert(p);
                _storage.Save(_bst.InOrder());
            }
        }

        public List<Product> SearchByName(string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return GetAll();
            q = q.Trim().ToLower();
            return GetAll().Where(x => (x.Name ?? "").ToLower().Contains(q) || (x.Description ?? "").ToLower().Contains(q)).ToList();
        }
    }
}
