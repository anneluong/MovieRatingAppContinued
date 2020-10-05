using MovieRatingApp.Core.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRatingApp.Core.ApplicationService.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            if (reviewer < 1)
                throw new ArgumentException("The id of the reviewer has to be larger than 0.");
            return _movieRepository.GetAll().Count(p => p.ReviewerId == reviewer);
        }

        public double GetAverageRateFromReviewer(int reviewer)
        {
            if (reviewer < 1)
                throw new ArgumentException("The id of the reviewer has to be larger than 0.");
            return _movieRepository.GetAll().Where(p => p.ReviewerId == reviewer).Average(p => p.Grade);
        }

        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            if (reviewer < 1)
                throw new ArgumentException("The id of the reviewer has to be larger than 0.");
            if (rate < 1 || rate > 5)
                throw new ArgumentException("The rate has to be within the range 1-5.");
            return _movieRepository.GetAll().Count(p => p.ReviewerId == reviewer && p.Grade == rate);
        }

        public int GetNumberOfReviews(int movie)
        {
            if (movie < 1)
                throw new ArgumentException("The id of the movie has to be larger than 0.");
            return _movieRepository.GetAll().Count(p => p.MovieId == movie);
        }

        public double GetAverageRateOfMovie(int movie)
        {
            if (movie < 1)
                throw new ArgumentException("The id of the movie has to be larger than 0.");
            return _movieRepository.GetAll().Where(p => p.MovieId == movie).Average(p => p.Grade);
        }

        public int GetNumberOfRates(int movie, int rate)
        {
            if (movie < 1)
                throw new ArgumentException("The id of the movie has to be larger than 0.");
            if (!(rate > 0 && rate < 6))
                throw new ArgumentException("The rate has to be within the range 1-5.");
            return _movieRepository.GetAll().Count(p => p.MovieId == movie && p.Grade == rate);
        }

        public List<int> GetMoviesWithHighestNumberOfTopRates() // RADO VERY DIFFERENT
        {
            return _movieRepository.GetAll()
                .OrderByDescending(p => GetNumberOfRates(p.MovieId, 5))
                .Select(p => p.MovieId)
                .Distinct()
                .ToList();
        }

        public List<int> GetMostProductiveReviewers()
        {
            return _movieRepository.GetAll()
                .GroupBy(p => p.ReviewerId)
                .OrderByDescending(p => p.Count())
                .Select(p => p.Key)
                .ToList();
        }

        public List<int> GetTopRatedMovies(int amount)
        {
            if (amount < 1)
                throw new ArgumentException("The amount has to be larger than 0.");
            return _movieRepository.GetAll()
                .OrderByDescending(p => GetAverageRateOfMovie(p.MovieId))
                .Select(p => p.MovieId)
                .Distinct()
                .Take(amount)
                .ToList();
        }

        public List<int> GetTopMoviesByReviewer(int reviewer) //RADO DIFFERENT
        {
            //ANNE DIFFERENT: 1 sec
            if (reviewer < 1)
                throw new ArgumentException("The id of the reviewer has to be larger than 0.");
            return _movieRepository.GetAll()
                .Where(p => p.ReviewerId == reviewer)
                .OrderByDescending(p => p.Grade)
                .ThenByDescending(p => p.Date)
                .Select(p => p.MovieId)
                .ToList();

            /*
            //RADO DIFFERENT: 2 sec
            var reviewersReviews = _movieRepository.GetAll()
                .Where(p => p.ReviewerId == reviewer)
                .OrderByDescending(p => p.Grade)
                .ThenByDescending(p => p.Date)
                .ThenBy(r => r.MovieId); //this line and below

            var idsList = new List<int>();

            foreach (var review in reviewersReviews)
                idsList.Add(review.MovieId);

            return idsList;
            */
        }

        public List<int> GetReviewersByMovie(int movie)
        {
            if (movie < 1)
                throw new ArgumentException("The id of the movie has to be larger than 0.");
            return _movieRepository.GetAll()
                .Where(p => p.MovieId == movie)
                .OrderByDescending(p => p.Grade)
                .ThenByDescending(p => p.Date)
                .Select(p => p.ReviewerId)
                .ToList();
        }
    }
}
