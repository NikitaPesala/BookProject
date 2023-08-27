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
        /*
        [HttpGet]
        public ViewResult Reviews()
        {
            var reviews = reviewsService.GetAllReviews();

            return View(reviews);
        }
        */

        public async Task<ViewResult> Reviews()
        {
            var reviews = await reviewsService.GetAllReviews();
            User user = new User()
            {
                Name = "Tejkiran Kushwaha",
                UserEmail = "tejkirankushwaha@outlook.com",
                Password = "123456",
                Photo = "https://c4.wallpaperflare.com/wallpaper/234/407/596/prince-of-persia-the-two-thrones-video-games-prince-of-persia-wallpaper-preview.jpg"

            };
            reviews[0].User = user;
            return View(reviews);
        }

        public async Task<ViewResult> Details(string id)
        {
            var reviews = await reviewsService.GetReviewsById(id);
            return View(reviews);
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

                //return RedirectToAction("Index", "BookController");
                //return RedirectToAction("Reviews");
                return View(reviews); ;
            }
            return View(reviews);
        }
        /*
        //when clicked on view reviews button, it should show the reviews page of that book and get the book id
        public async Task<ViewResult> ViewReviews(Book book)
        {
            var reviews = await reviewsService.GetReviewsByBookId(book.Id);
            return View(reviews);
        }

        //get reviews by book id
        [HttpGet] //getbookbyid
        public async Task<ViewResult> GetReviewsByBookId(string id)
        {
            var reviews = await reviewsService.GetReviewsById(id);
            return View(reviews);
        }
        */

        //return view
        

        

    }
}
