using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieRatingApp.Core.ApplicationService.Implementations;
using MovieRatingApp.Core.DomainService;
using MovieRatingApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRatingApp.Core.Tests.MSTest
{
    [TestClass]
    public class MovieServiceFunctionalTests
    {
        [TestMethod]
        public void TestNumberOfReviewsFromReviewer()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 2},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 5},
                new Rating {ReviewerId = 112, MovieId = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetNumberOfReviewsFromReviewer(reviewer: 111);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult == 2, "Number of reviews is NOT 2.");
        }

        [TestMethod]
        public void TestAverageRateFromReviewer()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 2},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 5},
                new Rating {ReviewerId = 112, MovieId = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetAverageRateFromReviewer(reviewer: 111);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult.Equals(3.5), "Average rate is not 3.5.");
        }

        [TestMethod]
        public void TestNumberOfRatesByReviewer()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 2},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 5},
                new Rating {ReviewerId = 111, MovieId = 3, Grade = 5},
                new Rating {ReviewerId = 112, MovieId = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetNumberOfRatesByReviewer(reviewer: 111, 5);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult == 2, "Number of rating 5 (from ReviewerId = 111) is NOT 2.");
        }

        [TestMethod]
        public void TestNumberOfReviews()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 2},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 5},
                new Rating {ReviewerId = 112, MovieId = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetNumberOfReviews(1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult == 2, "Number of reviews is NOT 2.");
        }

        [TestMethod]
        public void TestAverageRateOfMovie()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 2},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 5},
                new Rating {ReviewerId = 112, MovieId = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetAverageRateOfMovie(1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult.Equals(1.5), "Average rate is not 1.5.");
        }

        //This one is very slow compared to others. Find out why.
        [TestMethod]
        public void TestNumberOfRates()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 2},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 5},
                new Rating {ReviewerId = 111, MovieId = 3, Grade = 5},
                new Rating {ReviewerId = 112, MovieId = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetNumberOfRates(1, 1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult == 1, "Number of rating 1 (for MovieId = 1) is NOT 1.");
        }

        [TestMethod]
        public void TestMoviesWithHighestNumberOfTopRates() //RADO DIFFERENT
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 5},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 4},
                new Rating {ReviewerId = 111, MovieId = 3, Grade = 3},

                new Rating {ReviewerId = 112, MovieId = 1, Grade = 5},
                new Rating {ReviewerId = 112, MovieId = 2, Grade = 5},
                new Rating {ReviewerId = 112, MovieId = 3, Grade = 5},

                new Rating {ReviewerId = 113, MovieId = 1, Grade = 5},
                new Rating {ReviewerId = 113, MovieId = 2, Grade = 4},
                new Rating {ReviewerId = 113, MovieId = 3, Grade = 5},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetMoviesWithHighestNumberOfTopRates();

            //Assert
            mock.Verify(m => m.GetAll(), Times.Exactly(10));

            var expectedResult = new List<int>() { 1, 3, 2 };

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult), "Top 5 movies with highest number of rating 5 is not as expected.");
        }

        [TestMethod]
        public void TestMostProductiveReviewers()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 1},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 4},
                new Rating {ReviewerId = 111, MovieId = 3, Grade = 3},

                new Rating {ReviewerId = 112, MovieId = 1, Grade = 1},

                new Rating {ReviewerId = 113, MovieId = 1, Grade = 4},
                new Rating {ReviewerId = 113, MovieId = 2, Grade = 4},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetMostProductiveReviewers();

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            var expectedResult = new List<int>() { 111, 113, 112 };

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult), "The list of most productive reviewers is NOT { 111, 113, 112 }.");
        }

        [TestMethod]
        public void TestTopRatedMovies()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 5},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 2},
                new Rating {ReviewerId = 111, MovieId = 3, Grade = 3},

                new Rating {ReviewerId = 112, MovieId = 1, Grade = 4},
                new Rating {ReviewerId = 112, MovieId = 2, Grade = 4},

                new Rating {ReviewerId = 113, MovieId = 2, Grade = 4},
                new Rating {ReviewerId = 113, MovieId = 3, Grade = 4},
                new Rating {ReviewerId = 113, MovieId = 4, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetTopRatedMovies(3);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Exactly(9));

            var expectedResult = new List<int>() { 1, 3, 2 };

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult), "The list of top 3 rated movies is NOT { 1, 3, 2 }.");
        }

        [TestMethod]
        public void TestTopMoviesByReviewer() // RADO DIFFERENT
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 4, Date = DateTime.Parse("2020-03-11")},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 5, Date = DateTime.Parse("2002-03-12")},
                new Rating {ReviewerId = 111, MovieId = 3, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {ReviewerId = 112, MovieId = 1, Grade = 3, Date = DateTime.Parse("2020-03-12")},
                new Rating {ReviewerId = 112, MovieId = 2, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {ReviewerId = 113, MovieId = 2, Grade = 5, Date = DateTime.Parse("2020-03-12")},
                new Rating {ReviewerId = 113, MovieId = 3, Grade = 3, Date = DateTime.Parse("2020-03-12")},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetTopMoviesByReviewer(reviewer: 111);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            var expectedResult = new List<int>() { 2, 3, 1 };

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult), "The list of movies in order of rating (subsequently date) is NOT { 2, 3, 1 }.");

            //RADO DIFFERENT
            Assert.IsTrue(actualResult.ElementAt(0) == 2);
            Assert.IsTrue(actualResult.ElementAt(1) == 3);
            Assert.IsTrue(actualResult.ElementAt(2) == 1);
        }

        [TestMethod]
        public void TestReviewersByMovie()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {ReviewerId = 111, MovieId = 1, Grade = 4, Date = DateTime.Parse("2020-03-11")},
                new Rating {ReviewerId = 111, MovieId = 2, Grade = 5, Date = DateTime.Parse("2002-03-12")},
                new Rating {ReviewerId = 111, MovieId = 3, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {ReviewerId = 112, MovieId = 1, Grade = 4, Date = DateTime.Parse("2020-03-12")},
                new Rating {ReviewerId = 112, MovieId = 2, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {ReviewerId = 113, MovieId = 2, Grade = 5, Date = DateTime.Parse("2020-03-12")},
                new Rating {ReviewerId = 113, MovieId = 3, Grade = 3, Date = DateTime.Parse("2020-03-12")},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var ratingService = new MovieService(mock.Object);

            //Act
            var actualResult = ratingService.GetReviewersByMovie(movie: 1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            var expectedResult = new List<int>() { 112, 111 };

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult), "The list of reviewers in order of rating (subsequently date) is NOT { 112, 111 }.");

        }
    }
}
