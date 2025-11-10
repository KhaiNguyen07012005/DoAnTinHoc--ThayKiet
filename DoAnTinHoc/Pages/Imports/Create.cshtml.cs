using DoAnTinHoc.Data;
using DoAnTinHoc.DSA;
using DoAnTinHoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace DoAnTinHoc.Pages.Imports
{
    public class CreateModel : PageModel
    {
        private readonly ImportRepository _repo = new ImportRepository();
        private readonly EmployeeRepository _empRepo = new EmployeeRepository(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "employees.json")
        );
        public List<Product> Products { get; set; } = new();

        private readonly CategoryRepository _catRepo = new CategoryRepository(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "categories.json")
        );
        private readonly ProductRepository _prodRepo;

        public CreateModel()
        {
            _prodRepo = new ProductRepository(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.json"),
                _catRepo
            );
        }

        [BindProperty]
        public ImportRecord Record { get; set; } = new();

        [BindProperty]
        public List<ImportItem> Items { get; set; } = new();

        public List<Employee> Employees { get; set; } = new();

        public void OnGet()
        {
            Employees = _empRepo.GetAll();
            Products = _prodRepo.GetAll();

            if (Items.Count == 0)
                Items.Add(new ImportItem());
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            Record.Items = Items;
            _repo.Add(Record);

            return RedirectToPage("Index");
        }
    }
}
