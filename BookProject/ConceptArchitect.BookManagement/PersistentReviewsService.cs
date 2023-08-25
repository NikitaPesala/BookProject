using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentReviewsService : IReviewsService
    {
        IRepository<Reviews, string> repository;

        //constructor based DI
        public PersistentReviewsService(IRepository<Reviews, string> repository)
        {
            this.repository = repository;
        }


        public async Task<Reviews> AddReviews(Reviews Reviews)
        {
            //perform some validation if needed
            if (Reviews == null)
                throw new InvalidDataException("Reviews can't be null");

            if (string.IsNullOrEmpty(Reviews.Id))
            {
                Reviews.Id = await GenerateId(Reviews.User.Name);
            }

            return await repository.Add(Reviews);
        }

        private async Task<string> GenerateId(string name)
        {
            var id = name.ToLower().Replace(" ", "-");

            if (await repository.GetById(id) == null)
                return id;

            int d = 1;
            while (await repository.GetById($"{id}-{d}") != null)
                d++;

            return $"{id}-{d}";

        }

        public async Task DeleteReviews(string ReviewsId)
        {
            await repository.Delete(ReviewsId);
        }

        public async Task<List<Reviews>> GetAllReviews()
        {
            return await repository.GetAll();
        }

        public async Task<Reviews> GetReviewsById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task<List<Reviews>> SearchReviewss(string term)
        {
            term = term.ToLower();

            return await repository.GetAll(a => a.User.Name.ToLower().Contains(term) || a.Book.Title.ToLower().Contains(term));
        }

        public async Task<Reviews> UpdateReviews(Reviews Reviews)
        {

            return await repository.Update(Reviews, (old, newDetails) =>
            {
               
                old.Rating = newDetails.Rating;
                
                old.Details = newDetails.Details;
            });
        }
    }
}

