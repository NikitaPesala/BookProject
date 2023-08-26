using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using BookProject.ViewModels;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookProject
{
    public class BookController : Controller
    {
        IBookService bookService;
        IAuthorService authorService;
        public BookController(IBookService books, IAuthorService author)
        {
            this.bookService = books;
            this.authorService = author;
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
        public async Task<ViewResult> AddAsync()
        {
            AuthorController ac = new AuthorController(null);
            ac.ControllerContext = ControllerContext;
            List<Author> response = await authorService.GetAllAuthors();
            ViewBag.response = response;
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

		[HttpGet]
		public async Task<ViewResult> Update(string id)
		{
			var book = await bookService.GetBookById(id);
			var vm = new EditBookViewModel()
			{
				Id = book.Id,
				Title = book.Title,
				Description = book.Description,
				Price = book.Price,
				AuthorId = book.AuthorId,
				Tags =book.Tags,
				Cover = book.Cover
			};
			return View(vm);
		}

		[HttpPost]
		public async Task<ActionResult> Update(EditBookViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var book= new Book()
				{
					Id = vm.Id,
					Title = vm.Title,
					Description = vm.Description,
					Price = vm.Price,
					AuthorId = vm.AuthorId,
					Tags = vm.Tags,
					Cover = vm.Cover
				};

				await bookService.UpdateBook(book);
				return RedirectToAction("Index");
			}
			else
			{
				return View(vm);
			}
		}








		//public Book SaveV1(string id, string title, string description,string author_id,string cover_photo)
		//{
		//    var book = new Book()
		//    {
		//        Id = id,
		//        Title = title,
		//        Description = description,
		//        Author.Id = author_id,
		//        Cover = cover_photo

		//    };

		//    return book;
		//}



		//public Book SaveV0()
		//{
		//    var book = new Book()
		//    {



		//        Id = Request.Form["id"],
		//        Title = Request.Form["title"],
		//        Description = Request.Form["description"],
		//        Author_Id = Request.Form["author_id"],
		//        Cover_Photo = Request.Form["cover_photo"]





		//    };



		//    return book;
		//}
	}
}