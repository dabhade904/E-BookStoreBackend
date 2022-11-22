using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel CreateBook(BookModel book)
        {
            try
            {
                return bookRL.CreateBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                return bookRL.GetBookByBookId(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteBook(int bookId)
        {
            try
            {
                return bookRL.DeleteBook(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
