using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;



namespace BookProject
{
    public class BookController : Controller
    {
        IBookService bookService;
        public BookController(IBookService books)
        {
            this.bookService = books;
        }



        public async Task<ViewResult> Index()
        {
            var book = await bookService.GetAllBooks();



            return View(book);
        }



        public async Task<ViewResult> Details(string id)
        {
            var book = await bookService.GetBookById(id);



            return View(book);
        }

        [HttpGet]
        public async Task<ViewResult> Update()
        {
            return View(new Book());
        }

        [HttpPost]
        public async Task<ActionResult> Update(Book bookData)
        {
            Book book = await bookService.UpdateBook(bookData);
            return RedirectToAction("Index");
        }




        [HttpGet]
        public ViewResult Add()
        {
            return View(new Book());
        }



        [HttpPost]
        public async Task<ActionResult> Add(Book book)
        {
            await bookService.AddBook(book);



            return RedirectToAction("Index");
        }

		public async Task<ActionResult> Delete(string id)
		{
			await bookService.DeleteBook(id);
			return RedirectToAction("Index");
		}



		public async Task<ActionResult> SaveV2(Book book)
        {
            await bookService.AddBook(book);



            return RedirectToAction("Index");
        }








        public Book SaveV1(string id, string title, string description,string author_id,string cover_photo)
        {
            var book = new Book()
            {
                Id = id,
                Title = title,
                Description = description,
                Author_Id = author_id,
                Cover_Photo = cover_photo

            };

            return book;
        }



        public Book SaveV0()
        {
            var book = new Book()
            {



                Id = Request.Form["id"],
                Title = Request.Form["title"],
                Description = Request.Form["description"],
                Author_Id = Request.Form["author_id"],
                Cover_Photo = Request.Form["cover_photo"]





            };



            return book;
        }
    }
}