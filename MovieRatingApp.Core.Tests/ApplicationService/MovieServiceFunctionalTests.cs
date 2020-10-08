using Moq;
using MovieRatingApp.Core.ApplicationService.Implementations;
using MovieRatingApp.Core.DomainService;
using MovieRatingApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MovieRatingApp.Core.ApplicationService
{
    public class MovieServiceFunctionalTests
    {
        [Fact]
        public void TestNumberOfReviewsFromReviewer()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 2},
                new Rating {Reviewer = 111, Movie = 2, Grade = 5},
                new Rating {Reviewer = 112, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetNumberOfReviewsFromReviewer(reviewer: 111);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.True(actualResult == 2, "Number of reviews is NOT 2.");
        }

        [Fact]
        public void TestAverageRateFromReviewer()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 2},
                new Rating {Reviewer = 111, Movie = 2, Grade = 5},
                new Rating {Reviewer = 112, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetAverageRateFromReviewer(reviewer: 111);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.True(actualResult.Equals(3.5), "Average rate is not 3.5.");
        }

        [Fact]
        public void TestNumberOfRatesByReviewer()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 2},
                new Rating {Reviewer = 111, Movie = 2, Grade = 5},
                new Rating {Reviewer = 111, Movie = 3, Grade = 5},
                new Rating {Reviewer = 112, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetNumberOfRatesByReviewer(reviewer: 111, 5);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.True(actualResult == 2, "Number of rating 5 (from Reviewer = 111) is NOT 2.");
        }

        [Fact]
        public void TestNumberOfReviews()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 2},
                new Rating {Reviewer = 111, Movie = 2, Grade = 5},
                new Rating {Reviewer = 112, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetNumberOfReviews(1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.True(actualResult == 2, "Number of reviews is NOT 2.");
        }

        [Fact]
        public void TestAverageRateOfMovie()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 2},
                new Rating {Reviewer = 111, Movie = 2, Grade = 5},
                new Rating {Reviewer = 112, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetAverageRateOfMovie(1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.True(actualResult.Equals(1.5), "Average rate is not 1.5.");
        }

        //This one is very slow compared to others. Find out why.
        [Fact]
        public void TestNumberOfRates()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 2},
                new Rating {Reviewer = 111, Movie = 2, Grade = 5},
                new Rating {Reviewer = 111, Movie = 3, Grade = 5},
                new Rating {Reviewer = 112, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetNumberOfRates(1, 1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.True(actualResult == 1, "Number of rating 1 (for Movie = 1) is NOT 1.");
        }

        [Fact]
        public void TestMoviesWithHighestNumberOfTopRates() //RADO DIFFERENT
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 5},
                new Rating {Reviewer = 111, Movie = 2, Grade = 4},
                new Rating {Reviewer = 111, Movie = 3, Grade = 3},

                new Rating {Reviewer = 112, Movie = 1, Grade = 5},
                new Rating {Reviewer = 112, Movie = 2, Grade = 5},
                new Rating {Reviewer = 112, Movie = 3, Grade = 5},

                new Rating {Reviewer = 113, Movie = 1, Grade = 5},
                new Rating {Reviewer = 113, Movie = 2, Grade = 4},
                new Rating {Reviewer = 113, Movie = 3, Grade = 5},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetMoviesWithHighestNumberOfTopRates();

            //Assert
            //mock.Verify(m => m.GetAll(), Times.Exactly(10));
            //mock.Verify(m => m.GetAll(), Times.Exactly(4));

            var expectedResult = new List<int>() { 1, 3, 2 };

            Assert.True(actualResult.SequenceEqual(expectedResult), "Top 5 movies with highest number of rating 5 is not as expected.");
        }

        [Fact]
        public void TestMostProductiveReviewers()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 1},
                new Rating {Reviewer = 111, Movie = 2, Grade = 4},
                new Rating {Reviewer = 111, Movie = 3, Grade = 3},

                new Rating {Reviewer = 112, Movie = 1, Grade = 1},

                new Rating {Reviewer = 113, Movie = 1, Grade = 4},
                new Rating {Reviewer = 113, Movie = 2, Grade = 4},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetMostProductiveReviewers();

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            var expectedResult = new List<int>() { 111, 113, 112 };

            Assert.True(actualResult.SequenceEqual(expectedResult), "The list of most productive reviewers is NOT { 111, 113, 112 }.");
        }

        [Fact]
        public void TestTopRatedMovies()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 5},
                new Rating {Reviewer = 111, Movie = 2, Grade = 2},
                new Rating {Reviewer = 111, Movie = 3, Grade = 3},

                new Rating {Reviewer = 112, Movie = 1, Grade = 4},
                new Rating {Reviewer = 112, Movie = 2, Grade = 4},

                new Rating {Reviewer = 113, Movie = 2, Grade = 4},
                new Rating {Reviewer = 113, Movie = 3, Grade = 4},
                new Rating {Reviewer = 113, Movie = 4, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetTopRatedMovies(3);

            //Assert
            //mock.Verify(m => m.GetAll(), Times.Exactly(9));
            mock.Verify(m => m.GetAll(), Times.Once);

            var expectedResult = new List<int>() { 1, 3, 2 };

            Assert.True(actualResult.SequenceEqual(expectedResult), "The list of top 3 rated movies is NOT { 1, 3, 2 }.");
        }

        [Fact]
        public void TestTopMoviesByReviewer() // RADO DIFFERENT
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 4, Date = DateTime.Parse("2020-03-11")},
                new Rating {Reviewer = 111, Movie = 2, Grade = 5, Date = DateTime.Parse("2002-03-12")},
                new Rating {Reviewer = 111, Movie = 3, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {Reviewer = 112, Movie = 1, Grade = 3, Date = DateTime.Parse("2020-03-12")},
                new Rating {Reviewer = 112, Movie = 2, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {Reviewer = 113, Movie = 2, Grade = 5, Date = DateTime.Parse("2020-03-12")},
                new Rating {Reviewer = 113, Movie = 3, Grade = 3, Date = DateTime.Parse("2020-03-12")},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetTopMoviesByReviewer(reviewer: 111);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            var expectedResult = new List<int>() { 2, 3, 1 };

            Assert.True(actualResult.SequenceEqual(expectedResult), "The list of movies in order of rating (subsequently date) is NOT { 2, 3, 1 }.");

            //RADO DIFFERENT
            Assert.True(actualResult.ElementAt(0) == 2);
            Assert.True(actualResult.ElementAt(1) == 3);
            Assert.True(actualResult.ElementAt(2) == 1);
        }

        [Fact]
        public void TestReviewersByMovie()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 111, Movie = 1, Grade = 4, Date = DateTime.Parse("2020-03-11")},
                new Rating {Reviewer = 111, Movie = 2, Grade = 5, Date = DateTime.Parse("2002-03-12")},
                new Rating {Reviewer = 111, Movie = 3, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {Reviewer = 112, Movie = 1, Grade = 4, Date = DateTime.Parse("2020-03-12")},
                new Rating {Reviewer = 112, Movie = 2, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {Reviewer = 113, Movie = 2, Grade = 5, Date = DateTime.Parse("2020-03-12")},
                new Rating {Reviewer = 113, Movie = 3, Grade = 3, Date = DateTime.Parse("2020-03-12")},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetReviewersByMovie(movie: 1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            var expectedResult = new List<int>() { 112, 111 };

            Assert.True(actualResult.SequenceEqual(expectedResult), "The list of reviewers in order of rating (subsequently date) is NOT { 112, 111 }.");

        }
    }
}
