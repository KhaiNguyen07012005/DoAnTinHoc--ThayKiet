using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;

namespace DoAnTinHoc.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly CategoryRepository _repo;
        public EditModel(CategoryRepository repo) => _repo = repo;

        [BindProperty]
        public Category Category { get; set; } = new Category();

        public IActionResult OnGet(int id)
        {
            var c = _repo.GetById(id);
            if (c == null) return RedirectToPage("Index");
            Category = c;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            _repo.AddOrUpdate(Category);
            return RedirectToPage("Index");
        }
    }
}
