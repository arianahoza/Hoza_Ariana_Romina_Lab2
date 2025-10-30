using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hoza_Ariana_Romina_Lab2.Models;

namespace Hoza_Ariana_Romina_Lab2.Data
{
    public class Hoza_Ariana_Romina_Lab2Context : DbContext
    {
        public Hoza_Ariana_Romina_Lab2Context (DbContextOptions<Hoza_Ariana_Romina_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Hoza_Ariana_Romina_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Hoza_Ariana_Romina_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Hoza_Ariana_Romina_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Hoza_Ariana_Romina_Lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<Hoza_Ariana_Romina_Lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
