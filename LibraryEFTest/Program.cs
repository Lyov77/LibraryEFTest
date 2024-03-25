using Microsoft.EntityFrameworkCore;
using LibraryEFTest.Data.Concrete;
using LibraryEFTest.Data.Entities;

namespace LibraryEFTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
         

            using IBookRepository_UoW context = new IBookRepository_UoW();

            LibraryProcessor.ProcessLibrary(context);
        }
    }
}
