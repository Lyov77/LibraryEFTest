using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEFTest.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int PublicationYear {  get; set; }
        
        [MaxLength(100)]
        public string? Description { get; set; }


        [ForeignKey("Author")]
        public int? AuthorId {  get; set; }
        public virtual Author? Author { get; set; } 

    }
}
