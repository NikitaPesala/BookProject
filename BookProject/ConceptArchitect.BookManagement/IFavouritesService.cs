using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IFavouritesService
    {

        Task<List<Favourites>> GetAllFavourites();
        Task<Favourites> GetFavouritesById(string id);

        Task<Favourites> AddFavourites(Favourites Favourites);

         Task <Favourites> UpdateFavourites(Favourites Favourites);

        Task DeleteFavourites(string FavouritesId);
    }
}
