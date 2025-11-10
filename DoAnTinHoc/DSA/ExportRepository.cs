using DoAnTinHoc.Data;
using DoAnTinHoc.Models;
using System.Collections.Generic;

namespace DoAnTinHoc.DSA
{
    public class ExportRepository : JsonFileStorage<ExportRecord>
    {
        public ExportRepository() : base("data/exports.json") { }

        public List<ExportRecord> GetAllExports() => GetAllExports();
        public void AddExport(ExportRecord r)
        {
            var list = GetAllExports();
            r.Id = list.Count == 0 ? 1 : list.Max(x => x.Id) + 1;
            list.Add(r);
            Save(list);
        }
    }
}
