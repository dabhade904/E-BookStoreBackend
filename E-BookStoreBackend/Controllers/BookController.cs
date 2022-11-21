using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace E_BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [HttpPost("CreateBook")]
        public IActionResult Registration(BookModel bookModel)
        {
            try
            {
                var result = bookBL.CreateBook(bookModel);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Book created",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Something went wrong"
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet("GetBookById")]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var result = bookBL.GetBookByBookId(bookId);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Book Data Fetched",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Something went wrong"
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet("GetAllBook")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = bookBL.GetAllBooks();
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Book Data Fetched",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Something went wrong"
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
