using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;

namespace DoAnTinHoc.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProductRepository _repo;

        public DetailsModel(ProductRepository repo)
        {
            _repo = repo;
        }

        public Product? Product { get; set; }

        public IActionResult OnGet(int id)
        {
            // Bind ID từ query string (?id=1)
            Product = _repo.GetById(id);

            if (Product == null)
            {
                // Không tìm thấy → Redirect về Index với message
                TempData["Error"] = "Không tìm thấy sản phẩm.";
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}