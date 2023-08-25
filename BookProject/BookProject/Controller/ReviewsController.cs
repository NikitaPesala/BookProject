using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BookProject
{
    public class ReviewsController : Controller
    {
        IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviews)
        {
            this.reviewsService = reviews;
        }


        [HttpGet]
        public ViewResult Add()
        {
            return View(new Reviews());
        }

        [HttpPost]
        public async Task<ActionResult> Add(Reviews reviews)
        {
            if (ModelState.IsValid)
            {
                await reviewsService.AddReviews(reviews);

                return RedirectToAction("Index", "BookController");
            }
            return View(reviews);
        }
    }
}
