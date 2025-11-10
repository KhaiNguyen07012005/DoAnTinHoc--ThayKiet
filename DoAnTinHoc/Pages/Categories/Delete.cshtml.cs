using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;

namespace DoAnTinHoc.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly CategoryRepository _repo;
        public DeleteModel(CategoryRepository repo) => _repo = repo;

        [BindProperty]
        public Category? Category { get; set; }

        public IActionResult OnGet(int id)
        {
            Category = _repo.GetById(id);
            if (Category == null) return RedirectToPage("Index");
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.DeleteById(id);
            return RedirectToPage("Index");
        }
    }
}
