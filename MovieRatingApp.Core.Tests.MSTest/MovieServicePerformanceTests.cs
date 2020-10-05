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
        private static IMovieRepository movieRepository;

        [ClassInitialize]
        public static void SetupRepository(TestContext testContext)
        {
            movieRepository = new MovieRepositoryFileReader();
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfReviewsFromReviewerTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetNumberOfReviewsFromReviewer(1);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateFromReviewerTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetAverageRateFromReviewer(1);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfRatesByReviewerTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetNumberOfRatesByReviewer(1, 5);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfReviewsTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetNumberOfReviews(808731);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateOfMovieTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetAverageRateOfMovie(808731);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfRatesTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetNumberOfRates(808731, 3);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetMoviesWithHighestNumberOfTopRatesTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetMoviesWithHighestNumberOfTopRates();
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetMostProductiveReviewersTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetMostProductiveReviewers();
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetTopRatedMoviesTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetTopRatedMovies(10);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetTopMoviesByReviewerTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetTopMoviesByReviewer(1);
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetReviewersByMovieTest()
        {
            IMovieService movieService = new MovieService(movieRepository);
            movieService.GetReviewersByMovie(808731);
        }
    }
}
