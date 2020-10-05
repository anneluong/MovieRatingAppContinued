using MovieRatingApp.Core.Entities;
using System.Collections.Generic;

namespace MovieRatingApp.Core.DomainService
{
    public interface IMovieRepository
    {
        IEnumerable<Rating> GetAll();
    }
}
