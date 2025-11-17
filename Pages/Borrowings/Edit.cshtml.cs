using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hoza_Ariana_Romina_Lab2.Data;
using Hoza_Ariana_Romina_Lab2.Models;

namespace Hoza_Ariana_Romina_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Hoza_Ariana_Romina_Lab2.Data.Hoza_Ariana_Romina_Lab2Context _context;

        public EditModel(Hoza_Ariana_Romina_Lab2.Data.Hoza_Ariana_Romina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Borrowing == null)
    {
                return NotFound();
            }

            // 1. Încărcarea obiectului Borrowing, incluzând Book (și Author) și Member
            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (borrowing == null)
            {
                return NotFound();
            }
            Borrowing = borrowing;

            // 2. Pregătirea listei de cărți pentru Dropdown (ID și Titlu + Nume Autor)
            var bookList = _context.Book
                .Include(b => b.Author)
                .Select(x => new
                {
                    x.ID,
                    // Creează un șir cu Titlul cărții și Numele complet al Autorului
                    BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
                });

            // 3. Setarea ViewData pentru liste
            // Se folosește bookList (care conține detaliile Autorului) și Member (care are proprietatea FullName)
            ViewData["BookID"] = new SelectList(
                bookList,
                "ID",
                "BookFullName",
                Borrowing.BookID // Selectează valoarea curentă
            );
            ViewData["MemberID"] = new SelectList(
                _context.Member,
                "ID",
                "FullName",
                Borrowing.MemberID // Selectează valoarea curentă
            );

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
