using System.Collections.Generic;

namespace MovieRatingApp.Core.ApplicationService
{
    public interface IMovieService
    {
        int GetNumberOfReviewsFromReviewer(int reviewer);
        double GetAverageRateFromReviewer(int reviewer);
        int GetNumberOfRatesByReviewer(int reviewer, int rate);
        int GetNumberOfReviews(int movie);
        double GetAverageRateOfMovie(int movie);
        int GetNumberOfRates(int movie, int rate);
        List<int> GetMoviesWithHighestNumberOfTopRates();
        List<int> GetMostProductiveReviewers();
        List<int> GetTopRatedMovies(int amount);
        List<int> GetTopMoviesByReviewer(int reviewer);
        List<int> GetReviewersByMovie(int movie);
    }
}
