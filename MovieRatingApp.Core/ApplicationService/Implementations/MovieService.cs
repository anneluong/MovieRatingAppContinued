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
            return _movieRepository.GetAll().Count(p => p.Reviewer == reviewer);
        }

        public double GetAverageRateFromReviewer(int reviewer)
        {
            if (reviewer < 1)
                throw new ArgumentException("The id of the reviewer has to be larger than 0.");
            return _movieRepository.GetAll().Where(p => p.Reviewer == reviewer).Average(p => p.Grade);
        }

        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            if (reviewer < 1)
                throw new ArgumentException("The id of the reviewer has to be larger than 0.");
            if (rate < 1 || rate > 5)
                throw new ArgumentException("The rate has to be within the range 1-5.");
            return _movieRepository.GetAll().Count(p => p.Reviewer == reviewer && p.Grade == rate);
        }

        public int GetNumberOfReviews(int movie)
        {
            if (movie < 1)
                throw new ArgumentException("The id of the movie has to be larger than 0.");
            return _movieRepository.GetAll().Count(p => p.Movie == movie);
        }

        public double GetAverageRateOfMovie(int movie)
        {
            if (movie < 1)
                throw new ArgumentException("The id of the movie has to be larger than 0.");
            return _movieRepository.GetAll().Where(p => p.Movie == movie).Average(p => p.Grade);
            //Written slightly differently. Which is better for readability?
            //return _movieRepository.GetAll().Where(p => p.Movie == movie).Select(p => p.Grade).Average();
        }

        public int GetNumberOfRates(int movie, int rate)
        {
            if (movie < 1)
                throw new ArgumentException("The id of the movie has to be larger than 0.");
            if (!(rate > 0 && rate < 6))
                throw new ArgumentException("The rate has to be within the range 1-5.");
            return _movieRepository.GetAll().Count(p => p.Movie == movie && p.Grade == rate);
        }

        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            var allTopRatings = _movieRepository.GetAll().Where(p => p.Grade == 5);

            var dictionary = new Dictionary<int, int>();
            foreach (var rating in allTopRatings)
            {
                if (!dictionary.ContainsKey(rating.Movie))
                    dictionary.Add(rating.Movie, 1);
                else
                    ++dictionary[rating.Movie];
            }

            return dictionary.OrderByDescending(p => p.Value).Select(p => p.Key).ToList();
        }

        public List<int> GetMostProductiveReviewers()
        {
            return _movieRepository.GetAll()
                .GroupBy(p => p.Reviewer)
                .OrderByDescending(p => p.Count())
                .Select(p => p.Key)
                .ToList();
        }

        public List<int> GetTopRatedMovies(int amount)
        {
            if (amount < 1)
                throw new ArgumentException("The amount has to be larger than 0.");

            var allRatings = _movieRepository.GetAll();
            var accumulatedGradeDictionary = new Dictionary<int, double>();
            var countDictionary = new Dictionary<int, int>();
            foreach (var rating in allRatings)
            {
                if (!accumulatedGradeDictionary.ContainsKey(rating.Movie))
                {
                    accumulatedGradeDictionary.Add(rating.Movie, rating.Grade);
                    countDictionary.Add(rating.Movie, 1);
                }
                else
                {
                    var currentGrade = accumulatedGradeDictionary[rating.Movie];
                    accumulatedGradeDictionary[rating.Movie] = (currentGrade + rating.Grade);
                    ++countDictionary[rating.Movie];
                }
            }

            var myDictionary = new Dictionary<int, double>();
            foreach (var pair in accumulatedGradeDictionary)
            {
                var grade = accumulatedGradeDictionary[pair.Key];
                var count = countDictionary[pair.Key];

                myDictionary.Add(pair.Key, grade / count);
            }
            return myDictionary.OrderByDescending(x => x.Value).Select(p => p.Key).Take(amount).ToList();

            /* Too slow
            return _movieRepository.GetAll()
                .OrderByDescending(p => GetAverageRateOfMovie(p.Movie))
                .Select(p => p.Movie)
                .Take(amount)
                .ToList();
            */
        }

        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            //ANNE DIFFERENT: 1 sec
            if (reviewer < 1)
                throw new ArgumentException("The id of the reviewer has to be larger than 0.");
            return _movieRepository.GetAll()
                .Where(p => p.Reviewer == reviewer)
                .OrderByDescending(p => p.Grade)
                .ThenByDescending(p => p.Date)
                .Select(p => p.Movie)
                .ToList();

            /*
            //RADO DIFFERENT: 2 sec
            var reviewersReviews = _movieRepository.GetAll()
                .Where(p => p.Reviewer == reviewer)
                .OrderByDescending(p => p.Grade)
                .ThenByDescending(p => p.Date)
                .ThenBy(r => r.Movie); //this line and below

            var idsList = new List<int>();

            foreach (var review in reviewersReviews)
                idsList.Add(review.Movie);

            return idsList;
            */
        }

        public List<int> GetReviewersByMovie(int movie)
        {
            if (movie < 1)
                throw new ArgumentException("The id of the movie has to be larger than 0.");
            return _movieRepository.GetAll()
                .Where(p => p.Movie == movie)
                .OrderByDescending(p => p.Grade)
                .ThenByDescending(p => p.Date)
                .Select(p => p.Reviewer)
                .ToList();
        }
    }
}
