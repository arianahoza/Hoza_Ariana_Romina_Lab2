using System.ComponentModel.DataAnnotations;

namespace Hoza_Ariana_Romina_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
