using Hoza_Ariana_Romina_Lab2.Data;
using Hoza_Ariana_Romina_Lab2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoza_Ariana_Romina_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Hoza_Ariana_Romina_Lab2.Data.Hoza_Ariana_Romina_Lab2Context _context;

        public CreateModel(Hoza_Ariana_Romina_Lab2.Data.Hoza_Ariana_Romina_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var authorList = _context.Author.Select(x => new { x.ID, FullName = x.LastName + " " + x.FirstName });
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "LastName", "FirstName");
            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newBook = new Book();
            if (selectedCategories != null)
            { newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories) {
                    var catToAdd = new BookCategory 
                    { CategoryID = int.Parse(cat) };
                    newBook.BookCategories.Add(catToAdd);
                } 
            }
            Book.BookCategories = newBook.BookCategories;
            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
