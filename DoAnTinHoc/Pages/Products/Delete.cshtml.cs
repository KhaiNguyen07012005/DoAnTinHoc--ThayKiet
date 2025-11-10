using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;

namespace DoAnTinHoc.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ProductRepository _repo;
        public DeleteModel(ProductRepository repo) => _repo = repo;

        [BindProperty]
        public Product? Product { get; set; }

        public IActionResult OnGet(int id)
        {
            Product = _repo.GetById(id);
            if (Product == null) return RedirectToPage("Index");
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.DeleteById(id);
            return RedirectToPage("Index");
        }
    }
}
