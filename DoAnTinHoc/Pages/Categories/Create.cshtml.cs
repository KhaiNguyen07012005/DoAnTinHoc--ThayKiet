using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;

namespace DoAnTinHoc.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly CategoryRepository _repo;
        public CreateModel(CategoryRepository repo) => _repo = repo;

        [BindProperty]
        public Category Category { get; set; } = new Category();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            _repo.AddOrUpdate(Category);
            return RedirectToPage("Index");
        }
    }
}
