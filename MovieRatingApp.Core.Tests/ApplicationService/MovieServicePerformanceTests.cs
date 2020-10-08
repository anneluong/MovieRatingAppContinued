using MovieRatingApp.Core.ApplicationService.Implementations;
using MovieRatingApp.Core.DomainService;
using MovieRatingApp.Infrastructure.Static.Data.Repositories;
using System;
using Xunit;

namespace MovieRatingApp.Core.ApplicationService
{
    public class MovieServicePerformanceTests
    {
        private readonly IMovieRepository _movieRepository;

        public MovieServicePerformanceTests()
        {
            _movieRepository = new MovieRepositoryFileReader();
        }

        [Fact]
        public void GetNumberOfReviewsFromReviewer_PerformanceTest()
        {
            IMovieService movieService = new MovieService(_movieRepository);
            DateTime start = DateTime.Now;
            movieService.GetNumberOfReviewsFromReviewer(1);
            DateTime end = DateTime.Now;
            double time = (end - start).TotalMilliseconds;
            Assert.True(time <= 4000);
        }
    }
}
