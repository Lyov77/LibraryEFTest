using LibraryEFTest.Data.Abstracts;
using LibraryEFTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEFTest.Data.Concrete
{
    public class IBookRepository_UoW : IBookRepository
    {
        LibraryDBContext context = new LibraryDBContext();
        public void AddBook(Book book)
        {
            context.Books.Add(book);
        }

        public async Task AddBookAsync(Book book)
        {
            using LibraryDBContext context = new LibraryDBContext();

            await context.Books.AddAsync(book);

            await context.SaveChangesAsync();
        }

        public void UpdateBook(Book book)
        {
            using LibraryDBContext context = new LibraryDBContext();
            context.Books.Update(book);
            context.SaveChanges();
        }
        public async Task UpdateBookAsync(Book book)
        {
            using LibraryDBContext context = new LibraryDBContext();
            context.Books.Update(book);
            await context.SaveChangesAsync();
        }

        public void DeleteBook(int bookId)
        {
            using LibraryDBContext context = new LibraryDBContext();
            Book? book = context.Books.Find(bookId);

            if (book == null)
            {
                return;
            }

            context.Books.Remove(book);
            context.SaveChanges();
        }
        public async Task DeleteBookAsync(int bookId)
        {
            using LibraryDBContext context = new LibraryDBContext();
            Book? book = context.Books.Find(bookId);

            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
        }

        public Book? GetBookById(int bookId)
        {
            using LibraryDBContext context = new LibraryDBContext();
            return context.Books.Find(bookId);
        }

        public Book? GetBookByTitle(string title)
        {
            using (LibraryDBContext context = new LibraryDBContext())
            {
                return context.Books
                              .Include(b => b.Author) 
                              .FirstOrDefault(a => a.Title == title);
            }
        }

        public Author GetAuthorByNameAndSurname(string name, string surname)
        {
            using LibraryDBContext context = new LibraryDBContext();
            return context.Authors.FirstOrDefault(a => a.Name == name && a.Surname == surname)!;
        }



        public IQueryable<Book> GetAllBooks()
        {
            using LibraryDBContext context = new LibraryDBContext();
            return context.Books.AsQueryable();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
