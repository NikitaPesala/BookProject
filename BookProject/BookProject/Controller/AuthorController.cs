using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BookProject
{
	public class AuthorController : Controller
	{
		IAuthorService authorService;

		public AuthorController(IAuthorService authors)
		{
			this.authorService = authors;
		}

		public async Task<ViewResult> Index()
		{
			var authors = await authorService.GetAllAuthors();

			return View(authors);
		}

		public async Task<ViewResult> Details(string id)
		{
			var author = await authorService.GetAuthorById(id);

			return View(author);
		}


		[HttpGet]
		public ViewResult Add()
		{
			return View(new Author());
		}

		[HttpPost]
		public async Task<ActionResult> Add(Author author)
		{
			if (ModelState.IsValid)
			{
                await authorService.AddAuthor(author);

                return RedirectToAction("Index");
            }
			return View(author);
		}


		public async Task<ActionResult> SaveV2(Author author)
		{
			await authorService.AddAuthor(author);

			return RedirectToAction("Index");
		}

        [HttpGet]
        public async Task<ViewResult> Update(string id)
        {
			var author = await authorService.GetAuthorById(id);
			return View(author);

		}

		[HttpPost]
        public async Task<ActionResult> Update(Author authorData)
        {
            Author author = await authorService.UpdateAuthor(authorData);
            return RedirectToAction("Index");
        }
        


       
        public async Task<ActionResult> Delete(string id)
        {
            await authorService.DeleteAuthor(id);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public async Task<ActionResult> Delete(string id)
        //{
        //	await authorService.DeleteAuthor(id);

        //	Index();
        //	return RedirectToAction("Index");

        //}



        public Author SaveV1(string id, string name, string bio, string email, string photourl, DateTime dob)
		{
			var author = new Author()
			{
				Id = id,
				Name = name,
				Biography = bio,
				Email = email,
				BirthDate = dob,
				Photo = photourl
			};

			return author;
		}

		public Author SaveV0()
		{
			var author = new Author()
			{
				Id = Request.Form["id"],
				Name = Request.Form["name"],
				Biography = Request.Form["bio"],
				Email = Request.Form["email"],
				Photo = Request.Form["photourl"]
			};

			return author;
		}
	}
}
