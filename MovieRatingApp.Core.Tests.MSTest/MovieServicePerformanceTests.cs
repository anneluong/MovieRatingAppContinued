using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRatingApp.Core.ApplicationService;
using MovieRatingApp.Core.ApplicationService.Implementations;
using MovieRatingApp.Core.DomainService;
using MovieRatingApp.Infrastructure.Static.Data.Repositories;

namespace MovieRatingApp.Core.Tests.MSTest
{
    [TestClass]
    public class MovieServicePerformanceTests
    {
        private static IMovieRepository _movieRepository;

        [ClassInitialize]
        public static void SetupRepository(TestContext testContext)
        {
            _movieRepository = new MovieRepositoryFileReader();
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfReviewsFromReviewer_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetNumberOfReviewsFromReviewer(1);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateFromReviewer_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetAverageRateFromReviewer(1);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfRatesByReviewer_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetNumberOfRatesByReviewer(1, 5);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfReviews_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetNumberOfReviews(808731);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateOfMovie_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetAverageRateOfMovie(808731);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfRates_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetNumberOfRates(808731, 3);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetMoviesWithHighestNumberOfTopRates_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetMoviesWithHighestNumberOfTopRates();
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetMostProductiveReviewers_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetMostProductiveReviewers();
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetTopRatedMovies_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetTopRatedMovies(10);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetTopMoviesByReviewer_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetTopMoviesByReviewer(1);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetReviewersByMovie_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            movieService.GetReviewersByMovie(808731);
        }
    }
}
