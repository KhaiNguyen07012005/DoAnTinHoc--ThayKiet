using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.DSA;
using DoAnTinHoc.Models;
using System.Collections.Generic;
using System.Linq;

namespace DoAnTinHoc.Pages.Imports
{
    public class IndexModel : PageModel
    {
        public List<ImportRecord> Records { get; set; } = new();
        public Dictionary<int, string> EmployeeNames { get; set; } = new();

        private readonly ImportRepository _repo = new ImportRepository();
        private readonly EmployeeRepository _empRepo = new EmployeeRepository(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "employees.json")
        );

        public void OnGet()
        {
            Records = _repo.GetAllImports()
                           .OrderByDescending(r => r.Date)
                           .ToList();
            EmployeeNames = Records
                .Select(r => r.EmployeeId)
                .Distinct()
                .ToDictionary(id => id, id => _empRepo.GetById(id)?.Name ?? $"#ID{id}");
        }
    }
}
