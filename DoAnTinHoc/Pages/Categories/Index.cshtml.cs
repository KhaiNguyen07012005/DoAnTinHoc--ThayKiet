using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;
using System.Collections.Generic;

namespace DoAnTinHoc.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly CategoryRepository _repo;
        public IndexModel(CategoryRepository repo) => _repo = repo;

        public List<Category> Categories { get; set; } = new List<Category>();

        public void OnGet()
        {
            Categories = _repo.GetAll();
        }
    }
}
