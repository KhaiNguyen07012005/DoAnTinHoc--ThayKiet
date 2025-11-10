using DoAnTinHoc.Data;
using DoAnTinHoc.DSA;
using DoAnTinHoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoAnTinHoc.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly EmployeeRepository _repo;

        [BindProperty]
        public Employee Employee { get; set; } = new();

        public EditModel(IWebHostEnvironment env)
        {
            var file = Path.Combine(env.ContentRootPath, "data", "employees.json");
            _repo = new EmployeeRepository(file);
        }

        public void OnGet(int? id)
        {
            if (id.HasValue)
            {
                var ex = _repo.GetById(id.Value);
                if (ex != null)
                    Employee = ex;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _repo.AddOrUpdate(Employee);
            return RedirectToPage("Index");
        }
    }
}
