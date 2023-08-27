using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksProject
{
    public class UserController : Controller
    {
        IUserService authorService;

        public UserController(IUserService authors)
        {
            this.authorService = authors;
        }

        public async Task<ViewResult> Index(string name)
        {
            var authors = await authorService.GetAllUser();

            return View(authors);
        }

        public async Task<ViewResult> Details(string id)
        {
            var author = await authorService.GetUserById(id);

            return View(author);
        }
        public async Task<ViewResult> Search(string term)
        {
            var response = await authorService.SearchUser(term);
            return View("Index", response);
        }
        public async Task<ActionResult> Delete(string id)
        {
            await authorService.DeleteUser(id);

            return RedirectToAction("Index");
        }
        public async Task<ViewResult> Edit(string id)
        {
            var author = await authorService.GetUserById(id);
            return View(author);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(User author)
        {
            await authorService.UpdateUser(author);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View(new User());
        }
        [HttpGet]
        public ViewResult Login()
        {
            return View(new User());
        }
        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            var response = await authorService.GetUserById(user.UserEmail);
            if (response == null || response.Password != user.Password)
            {
                return View(new User());
            }
            return RedirectToAction("Index", "Home", null);
        }

        [HttpPost]
        public async Task<ActionResult> Add(User user)
        {
            await authorService.AddUser(user);

            return RedirectToAction("Index", "Home", user.Name);
        }


        public async Task<ActionResult> SaveV2(User author)
        {
            await authorService.AddUser(author);

            return RedirectToAction("Index");
        }



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
