using DoAnTinHoc.Data;
using DoAnTinHoc.DSA;
using DoAnTinHoc.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoAnTinHoc.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly EmployeeRepository _repo;

        public List<Employee> Employees { get; set; } = new();
        public string? Keyword { get; set; }
        public string? Status { get; set; }

        public IndexModel(IWebHostEnvironment env)
        {
            var file = Path.Combine(env.ContentRootPath, "data", "employees.json");
            _repo = new EmployeeRepository(file);
        }

        public void OnGet(string? keyword, string? status)
        {
            Keyword = keyword;
            Status = status;
            Employees = _repo.GetAll();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim().ToLower();
                Employees = Employees.Where(e =>
                    e.Name.ToLower().Contains(keyword) ||
                    e.Position.ToLower().Contains(keyword)
                ).ToList();
            }

           
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (status == "active")
                    Employees = Employees.Where(e => e.Active).ToList();
                else if (status == "inactive")
                    Employees = Employees.Where(e => !e.Active).ToList();
            }
        }
    }
}
