using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore_Api.Contracts;
using BookStore_Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Api.Services
{
    public class BookRepository : iBookRepository

    {


        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<bool> Create(Book entity)
        {
            await _db.Books.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Book entity)
        {
            _db.Books.Remove(entity);
            return await Save();
        }

        public async Task<IList<Book>> FindAll()
        {
            var books = await _db.Books
                 .Include(q => q.Author)
                 .ToListAsync();
            return books;
        }

        public async Task<Book> FindByID(int id)
        {
            var book = await _db.Books
                  .Include(q => q.Author)
                  .FirstOrDefaultAsync(q => q.Id == id);
            return book;
        }


        //public async Task<string> GetImageFileName(int id)
        //{
        //    var book = await _db.Books
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(q => q.Id == id);
        //    return book.Image;
        //}

        public async Task<bool> isExists(int id)
        {
            return await _db.Books.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Book entity)
        {

            _db.Books.Update(entity);
            return await Save();
        }
    }
}
