using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.DSA;
using DoAnTinHoc.Models;

namespace DoAnTinHoc.Pages.Imports
{
    public class DetailsModel : PageModel
    {
        private readonly ImportRepository _repo = new ImportRepository();
        private readonly EmployeeRepository _empRepo = new EmployeeRepository(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "employees.json")
        );

        [BindProperty]
        public ImportRecord? Record { get; set; }
        public string EmployeeName { get; set; } = "";

        public IActionResult OnGet(int id)
        {
            Record = _repo.GetAllImports().FirstOrDefault(r => r.Id == id);
            if (Record == null) return NotFound();

            EmployeeName = _empRepo.GetById(Record.EmployeeId)?.Name ?? $"#ID{Record.EmployeeId}";
            return Page();
        }
    }
}
