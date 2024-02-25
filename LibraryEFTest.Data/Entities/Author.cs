using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEFTest.Data.Entities
{
    public class Author
    {

        public Author()
        {
            Books = new List<Book>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Mark 'Id' as auto-generated identity column
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Surname { get; set; } = "";

        public virtual ICollection<Book> Books { get; set; }
    }
}
