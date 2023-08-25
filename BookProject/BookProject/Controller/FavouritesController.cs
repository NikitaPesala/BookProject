using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BookProject
{
    public class FavouritesController : Controller
    {
        IFavouritesService favouritesService;

        public FavouritesController(IFavouritesService favourites)
        {
            this.favouritesService = favourites;
        }


        [HttpGet]
        public ViewResult Add()
        {
            return View(new Favourites());
        }

        [HttpPost]
        public async Task<ActionResult> Add(Favourites favourites)
        {
            if (ModelState.IsValid)
            {
                await favouritesService.AddFavourites(favourites);

                return RedirectToAction("Index");
            }
            return View(favourites);
        }
    }
}
