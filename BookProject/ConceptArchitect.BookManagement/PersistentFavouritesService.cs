using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentFavouritesService : IFavouritesService
    {
        IRepository<Favourites, string> repository;

        //constructor based DI
        public PersistentFavouritesService(IRepository<Favourites, string> repository)
        {
            this.repository = repository;
        }


        public async Task<Favourites> AddFavourites(Favourites Favourites)
        {
            //perform some validation if needed
            if (Favourites == null)
                throw new InvalidDataException("Favourites can't be null");

            if (string.IsNullOrEmpty(Favourites.Id))
            {
                Favourites.Id = await GenerateId(Favourites.User.Name);
            }

            return await repository.Add(Favourites);
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

        public async Task DeleteFavourites(string FavouritesId)
        {
            await repository.Delete(FavouritesId);
        }

        public async Task<List<Favourites>> GetAllFavourites()
        {
            return await repository.GetAll();
        }

        public async Task<Favourites> GetFavouritesById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task<List<Favourites>> SearchFavourites(string term)
        {
            term = term.ToLower();

            return await repository.GetAll(a => a.Book.Title.ToLower().Contains(term) );
        }

        public async Task<Favourites> UpdateFavourites(Favourites favourites)
        {
            return null;
        }

       
    }
}

