using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel CreateBook(BookModel book);
        public BookModel GetBookByBookId(int bookId);
        public List<BookModel> GetAllBooks();
        public bool DeleteBook(int BookId);
        bool UpdateBook(int BookId, BookModel updateBook);
    }
}
