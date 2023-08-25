using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IReviewsService
    {
        Task<List<Reviews>> GetAllReviews();
        Task<Reviews> GetReviewsById(string id);

        Task<Reviews> AddReviews(Reviews Reviews);

        Task<Reviews> UpdateReviews(Reviews Reviews);

        Task DeleteReviews(string ReviewsId);

        Task<List<Reviews>> SearchReviewss(string term);


    }
}
