using DoAnTinHoc.Models;
using DoAnTinHoc.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnTinHoc.DSA
{
    public class ImportRepository
    {
        private readonly JsonFileStorage<ImportRecord> _store;
        private static readonly object _fileLock = new object();

        public ImportRepository() => _store = new JsonFileStorage<ImportRecord>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "imports.json"));

        public List<ImportRecord> GetAll()
        {
            lock (_fileLock) return _store.Load();
        }

        public void Add(ImportRecord record)
        {
            lock (_fileLock)
            {
                var list = _store.Load() ?? new List<ImportRecord>();
                record.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
                record.Date = record.Date == default ? DateTime.Now : record.Date;
                list.Add(record);
                _store.Save(list);
            }   
        }
        public List<ImportRecord> GetAllImports() => GetAll();
    }
}
