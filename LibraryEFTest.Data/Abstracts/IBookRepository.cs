using LibraryEFTest.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEFTest.Data.Abstracts
{
    public interface IBookRepository : IDisposable
    {
        void AddBook(Book book);
        Task AddBookAsync(Book book);

        void UpdateBook(Book book);
        Task UpdateBookAsync(Book book);

        void DeleteBook(int bookId);
        Task DeleteBookAsync(int bookId);

        Book? GetBookById(int bookId);
        Author GetAuthorByNameAndSurname(string name, string surname);

        IQueryable<Book> GetAllBooks();

        void SaveChanges();
    }
}

