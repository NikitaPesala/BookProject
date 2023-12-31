﻿using ConceptArchitect.BookManagement;
using BookProject.ViewModels;
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
			var vm = new EditAuthorViewModel()
			{
				Id = author.Id,
				Name = author.Name,
				Email= author.Email,
				Biography = author.Biography,
				BirthDate = author.BirthDate,
				DeathDate = author.DeathDate,
				Tags = author.Tags,
				Photo = author.Photo
			};
			return View(vm);

		}

		[HttpPost]
        public async Task<ActionResult> Update(EditAuthorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var author = new Author()
                {
                    Id = vm.Id,
                    Name = vm.Name,
					Email = vm.Email,
                    Biography = vm.Biography,
                    BirthDate = vm.BirthDate,
                    DeathDate = vm.DeathDate,
                    Tags = vm.Tags,
                    Photo = vm.Photo
                };

                await authorService.UpdateAuthor(author);
                return RedirectToAction("Index");
            }
			else
			{
                return View(vm);
            }
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
