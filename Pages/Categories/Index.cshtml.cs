using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hoza_Ariana_Romina_Lab2.Data;
using Hoza_Ariana_Romina_Lab2.Models;
using Hoza_Ariana_Romina_Lab2.Models.ViewModels;


namespace Hoza_Ariana_Romina_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Hoza_Ariana_Romina_Lab2.Data.Hoza_Ariana_Romina_Lab2Context _context;

        public IndexModel(Hoza_Ariana_Romina_Lab2.Data.Hoza_Ariana_Romina_Lab2Context context)
        {
            _context = context;
        }
        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            CategoryData = new CategoryIndexData();
            Category = await _context.Category.ToListAsync();
            CategoryData.Categories = await _context.Category
               .Include(c => c.BookCategories)
                   .ThenInclude(bc => bc.Book)
                       .ThenInclude(b => b.Author)
               .OrderBy(c => c.CategoryName)
               .ToListAsync();
            if (id != null)
            {
                CategoryID = id.Value;

                var category = CategoryData.Categories
                    .Where(c => c.ID == id.Value)
                    .Single();
                CategoryData.Books = category.BookCategories
                   .Select(bc => bc.Book);
            }
        }
    }
}
