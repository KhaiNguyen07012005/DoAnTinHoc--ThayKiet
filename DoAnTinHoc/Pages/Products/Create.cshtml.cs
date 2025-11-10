using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;

namespace DoAnTinHoc.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProductRepository _repo;
        private readonly CategoryRepository _catRepo;  // Inject để load categories

        public CreateModel(ProductRepository repo, CategoryRepository catRepo)
        {
            _repo = repo;
            _catRepo = catRepo;
        }

        [BindProperty]
        public Product Product { get; set; } = new();  // Init rỗng để bind

        public List<Category> Categories { get; set; } = new();  // Cho select options

        public void OnGet()
        {
            Console.WriteLine("OnGet Create: Loading categories...");  // LOG
            Categories = _catRepo.GetAll();  // Load từ repo Category (giả sử có GetAll())
            Console.WriteLine($"Loaded {Categories.Count} categories.");  // LOG
        }

        // Đổi thành OnPost (sync) vì repo sync
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost called! Product data: " + Product.Name + ", CategoryId: " + Product.CategoryId);  // LOG input

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState invalid! Errors: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));  // LOG errors
                Categories = _catRepo.GetAll();  // Reload categories
                return Page();  // Reload form + show errors
            }

            if (Product == null || string.IsNullOrWhiteSpace(Product.Name))
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return Page();
            }

            // Optional: Validate Category tồn tại
            if (Product.CategoryId.HasValue && !_catRepo.GetAll().Any(c => c.Id == Product.CategoryId.Value))
            {
                ModelState.AddModelError("Product.CategoryId", "Danh mục không tồn tại.");
                return Page();
            }

            // Gọi repo AddOrUpdate (sync)
            _repo.AddOrUpdate(Product);

            Console.WriteLine($"AddOrUpdate success for: {Product.Name} (ID: {Product.Id})");  // LOG success

            TempData["Success"] = "Tạo sản phẩm thành công!";
            return RedirectToPage("./Index");
        }
    }
}