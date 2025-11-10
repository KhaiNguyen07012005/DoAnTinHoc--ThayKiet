using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAnTinHoc.Data;
using DoAnTinHoc.Models;
using System.Collections.Generic;
using System.Linq;

namespace DoAnTinHoc.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductRepository _repo;
        public IndexModel(ProductRepository repo) => _repo = repo;

        public List<Product> Products { get; set; } = new List<Product>();

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; } = string.Empty;
     
        public int TotalProducts => Products.Count;

        public decimal TotalValue => Products.Sum(p => p.Price * p.Quantity);

        public decimal AvgPrice => Products.Any() ? Products.Average(p => p.Price) : 0;

        public List<object> ChartData => Products
            .Select(p => (object)new { Label = p.Name, Value = p.Price * p.Quantity })
            .ToList();

        public void OnGet()
        {
            var all = _repo.GetAll() ?? new List<Product>();
            if (!string.IsNullOrWhiteSpace(Search))
            {
                Products = all.Where(p => (!string.IsNullOrEmpty(p.Name) && p.Name.Contains(Search, System.StringComparison.CurrentCultureIgnoreCase))
                                       || (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(Search, System.StringComparison.CurrentCultureIgnoreCase)))
                              .ToList();
            }
            else
            {
                Products = all;
            }
        }
    }
}
