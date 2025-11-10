using DoAnTinHoc.Data;
using DoAnTinHoc.DSA;
using DoAnTinHoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoAnTinHoc.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly EmployeeRepository _repo;

        public Employee Employee { get; set; } = new();

        public DeleteModel(IWebHostEnvironment env)
        {
            var file = Path.Combine(env.ContentRootPath, "data", "employees.json");
            _repo = new EmployeeRepository(file);
        }

        public void OnGet(int id)
        {
            Employee = _repo.GetById(id) ?? new Employee();
        }

        public IActionResult OnPost(int id)
        {
            _repo.DeleteById(id);
            return RedirectToPage("Index");
        }
    }
}
