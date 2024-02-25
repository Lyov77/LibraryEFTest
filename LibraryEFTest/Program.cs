using Microsoft.EntityFrameworkCore;
using LibraryEFTest.Data.Concrete;
using LibraryEFTest.Data.Entities;

namespace LibraryEFTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // using LibraryDBContex_Uow context = new LibraryDBContext();

            using IBookRepository_UoW context = new IBookRepository_UoW();

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - Add book");
                Console.WriteLine("2 - Find book");
                Console.WriteLine("3 - Show All Books");
                Console.WriteLine("4 - Show Author's all books");
                Console.WriteLine("0 - Exit");
                Console.WriteLine();

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Book title: ");
                        string title = Console.ReadLine();
                        Console.Write("Publication Year: ");
                        int year = Int32.Parse(Console.ReadLine());
                        Console.Write("Book description: ");
                        string description = Console.ReadLine();
                        Console.Write("Author's Name: ");
                        string authorName = Console.ReadLine();
                        Console.Write("Author's Surname: ");
                        string authorSurname = Console.ReadLine();

                        var existingAuthor = context.GetAuthorByNameAndSurname(authorName, authorSurname);
                        var existingAuthorId = existingAuthor.Id;

                        if (existingAuthor != null)
                        {
                            context.AddBook(new Book
                            {
                                Title = title,
                                PublicationYear = year,
                                Description = description,
                                AuthorId = existingAuthor.Id // Use existing author
                            });
                        }
                        else
                        {
                            var newAuthor = new Author
                            {
                                Name = authorName,
                                Surname = authorSurname
                            };

                            context.AddBook(new Book
                            {
                                Title = title,
                                PublicationYear = year,
                                Description = description,
                                Author = newAuthor // Use newly created author
                            });
                        }
                        context.SaveChanges();
                        Console.WriteLine($"Book '{title}' by {authorName} {authorSurname} added.");
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.Write("Book Title: ");
                        string bookTitle = Console.ReadLine();
                        Book foundBook = context.GetBookByTitle(bookTitle);

                        if (foundBook == null)
                        {
                            Console.WriteLine("No book found");
                        }
                        else
                        {
                            Console.WriteLine($"Book found: '{foundBook.Title}' by {foundBook.Author.Name} {foundBook.Author.Surname}");
                        }
                        Console.WriteLine();
                        break;

                    case "3":
                        using (var dbContext = new LibraryDBContext())
                        {
                            var books = dbContext.Books.Include(b => b.Author).ToList();
                            int i = 1;
                            foreach (var book in books)
                            {
                                Console.WriteLine($"{i}. '{book.Title}' by {book.Author.Name} {book.Author.Surname}, published in {book.PublicationYear}");
                                i++;
                            }
                            Console.WriteLine();
                        }
                        break;

                    case "4":
                        Console.Write("Author's name: ");
                        string authorsName = Console.ReadLine();
                        Console.Write("Author's surname: ");
                        string authorsSurname = Console.ReadLine();
                        Author foundAuthor = context.GetAuthorByNameAndSurname(authorsName, authorsSurname);

                        if (foundAuthor != null)
                        {
                            using (var dbContext = new LibraryDBContext())
                            {
                                dbContext.Entry(foundAuthor).Collection(a => a.Books).Load();
                                Console.WriteLine($"Books by {foundAuthor.Name} {foundAuthor.Surname}:");
                                int i = 1;
                                foreach (var book in foundAuthor.Books)
                                {
                                    Console.WriteLine($"{i}. {book.Title}");
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Author {authorsName} {authorsSurname} not found.");
                        }
                        Console.WriteLine();
                        break;

                    case "0":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;

                }
            }
        }
    }
}
