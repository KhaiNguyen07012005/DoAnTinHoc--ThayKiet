using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;
using System.Collections.Generic;

namespace DoAnTinHoc.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ProductRepository _repo;
        private readonly CategoryRepository _catRepo;
        public EditModel(ProductRepository repo, CategoryRepository catRepo) { _repo = repo; _catRepo = catRepo; }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public List<Category> Categories { get; set; } = new List<Category>();

        public IActionResult OnGet(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return RedirectToPage("Index");
            Product = p;
            Categories = _catRepo.GetAll();
            return Page();
        }
        public IActionResult OnPost(int id)  
        {
            Console.WriteLine($"OnPost Edit: ID {id}, Product: {Product?.Name}");  // LOG

            if (id != Product?.Id || Product == null)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return RedirectToPage("./Index");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Invalid: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));  // LOG
                Categories = _catRepo.GetAll();  // Reload nếu invalid
                return Page();
            }

            // THÊM VALIDATION CATEGORY
            if (Product.CategoryId.HasValue && !_catRepo.GetAll().Any(c => c.Id == Product.CategoryId.Value))
            {
                ModelState.AddModelError("Product.CategoryId", "Danh mục không tồn tại.");
                Categories = _catRepo.GetAll();
                return Page();
            }

            _repo.AddOrUpdate(Product);

            TempData["Success"] = "Cập nhật thành công!";
            return RedirectToPage("./Index");
        }
    }
}
