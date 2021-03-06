﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void GetNumberOfReviewsFromReviewer_InvalidInput_ThrowsArgumentException()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);
            const int invalidInput = 0;

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetNumberOfReviewsFromReviewer(invalidInput));
        }

        [TestMethod]
        public void GetNumberOfReviewsFromReviewer_ReturnsCorrectResults()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 1, Movie = 1, Grade = 2},
                new Rating {Reviewer = 1, Movie = 2, Grade = 5},
                new Rating {Reviewer = 2, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var movieService = new MovieService(mock.Object);

            const int expectedResult = 2;

            //Act
            var actualResult = movieService.GetNumberOfReviewsFromReviewer(reviewer: 1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult == expectedResult, "Number of reviews is NOT as expected.");
        }

        [TestMethod]
        public void GetAverageRateFromReviewer_InvalidInput_ThrowsArgumentException()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);
            const int invalidInput = 0;

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetAverageRateFromReviewer(invalidInput));
        }

        [TestMethod]
        public void GetAverageRateFromReviewer_ReturnsCorrectResults()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 1, Movie = 1, Grade = 2},
                new Rating {Reviewer = 1, Movie = 2, Grade = 5},
                new Rating {Reviewer = 2, Movie = 1, Grade = 1},
                new Rating {Reviewer = 2, Movie = 2, Grade = 2},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var movieService = new MovieService(mock.Object);

            var expectedResult = 3.5;

            //Act
            var actualResult = movieService.GetAverageRateFromReviewer(reviewer: 1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult.Equals(expectedResult), "Average rate is NOT as expected.");
        }

        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(1, 6)]
        [DataTestMethod]
        public void GetNumberOfRatesByReviewer_InvalidInput_ThrowsArgumentException(int inputReviewer, int inputRate)
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetNumberOfRatesByReviewer(inputReviewer, inputRate));
        }

        [DataRow(1, 1, 0)]
        [DataRow(1, 5, 2)]
        [DataTestMethod]
        public void GetNumberOfRatesByReviewer_ReturnsCorrectResults(int inputReviewer, int inputRate,
            int expectedResult)
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 1, Movie = 1, Grade = 2},
                new Rating {Reviewer = 1, Movie = 2, Grade = 5},
                new Rating {Reviewer = 1, Movie = 3, Grade = 5},
                new Rating {Reviewer = 2, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var movieService = new MovieService(mock.Object);

            //Act
            var actualResult = movieService.GetNumberOfRatesByReviewer(reviewer: inputReviewer, inputRate);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult == expectedResult, "Number of rating 5 is NOT as expected.");
        }

        [TestMethod]
        public void GetNumberOfReviews_InvalidInput_ThrowsArgumentException()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);
            const int invalidInput = 0;

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetNumberOfReviews(invalidInput));
        }

        [TestMethod]
        public void GetNumberOfReviews_ReturnsCorrectResults()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 1, Movie = 1, Grade = 2},
                new Rating {Reviewer = 1, Movie = 2, Grade = 5},
                new Rating {Reviewer = 2, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var movieService = new MovieService(mock.Object);

            const int expectedResult = 2;

            //Act
            var actualResult = movieService.GetNumberOfReviews(1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult == expectedResult, "Number of reviews is NOT as expected.");
        }

        [TestMethod]
        public void GetAverageRateOfMovie_InvalidInput_ThrowsArgumentException()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);
            const int invalidInput = 0;

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetAverageRateOfMovie(invalidInput));
        }

        [TestMethod]
        public void GetAverageRateOfMovie_ReturnsCorrectResults()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 1, Movie = 1, Grade = 2},
                new Rating {Reviewer = 1, Movie = 2, Grade = 5},
                new Rating {Reviewer = 2, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var movieService = new MovieService(mock.Object);

            const double expectedResult = 1.5;

            //Act
            var actualResult = movieService.GetAverageRateOfMovie(1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult.Equals(expectedResult), "Average rate is NOT as expected.");
        }

        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(1, 6)]
        [DataTestMethod]
        public void GetNumberOfRates_InvalidInput_ThrowsArgumentException(int inputMovie, int inputRate)
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetNumberOfRates(inputMovie, inputRate));
        }

        [DataRow(1, 1, 2)]
        [DataRow(1, 5, 0)]
        [DataTestMethod]
        public void GetNumberOfRates_ReturnsCorrectResults(int inputMovie, int inputRate, int expectedResult)
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 1, Movie = 1, Grade = 2},
                new Rating {Reviewer = 1, Movie = 2, Grade = 5},
                new Rating {Reviewer = 2, Movie = 1, Grade = 1},
                new Rating {Reviewer = 3, Movie = 1, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var movieService = new MovieService(mock.Object);

            //Act
            var actualResult = movieService.GetNumberOfRates(inputMovie, inputRate);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult == expectedResult,
                "Number of rating for the respective movies is NOT as expected.");
        }

        [TestMethod]
        public void GetMoviesWithHighestNumberOfTopRates_ReturnsCorrectResults() //RADO DIFFERENT
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

            var movieService = new MovieService(mock.Object);

            var expectedResult = new List<int>() { 1, 3, 2 };

            //Act
            var actualResult = movieService.GetMoviesWithHighestNumberOfTopRates();

            //Assert
            //mock.Verify(m => m.GetAll(), Times.Exactly(10));
            //mock.Verify(m => m.GetAll(), Times.Exactly(4));

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult),
                "Top movies with highest number of rating is NOT as expected.");
        }

        [TestMethod]
        public void GetMostProductiveReviewers_ReturnsCorrectResults()
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

            var movieService = new MovieService(mock.Object);

            var expectedResult = new List<int>() { 111, 113, 112 };

            //Act
            var actualResult = movieService.GetMostProductiveReviewers();

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult),
                "The list of most productive reviewers is NOT as expected.");
        }

        [TestMethod]
        public void GetTopRatedMovies_ThrowsArgumentException()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);
            const int invalidInput = 0;

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetTopRatedMovies(invalidInput));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public void GetTopRatedMovies_ReturnsCorrectResults(int inputAmount, IEnumerable<int> expectedResult)
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 1, Movie = 1, Grade = 5},
                new Rating {Reviewer = 1, Movie = 2, Grade = 2},
                new Rating {Reviewer = 1, Movie = 3, Grade = 3},

                new Rating {Reviewer = 2, Movie = 1, Grade = 4},
                new Rating {Reviewer = 2, Movie = 2, Grade = 4},

                new Rating {Reviewer = 3, Movie = 2, Grade = 4},
                new Rating {Reviewer = 3, Movie = 3, Grade = 4},
                new Rating {Reviewer = 3, Movie = 4, Grade = 1},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var movieService = new MovieService(mock.Object);

            //Act
            var actualResult = movieService.GetTopRatedMovies(inputAmount);

            //Assert
            //mock.Verify(m => m.GetAll(), Times.Exactly(9));
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult),
                "The list of top rated movies is NOT as expected.");
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { 1, new List<int>() { 1 } };
            yield return new object[] { 3, new List<int>() { 1, 3, 2 } };
        }

        [TestMethod]
        public void GetTopMoviesByReviewer_ThrowsArgumentException()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);
            const int invalidInput = 0;

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetTopMoviesByReviewer(invalidInput));
        }

        [TestMethod]
        public void GetTopMoviesByReviewer_ReturnsCorrectResults() // RADO DIFFERENT
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Rating[] returnValue =
            {
                new Rating {Reviewer = 1, Movie = 1, Grade = 4, Date = DateTime.Parse("2020-03-11")},
                new Rating {Reviewer = 1, Movie = 2, Grade = 5, Date = DateTime.Parse("2002-03-12")},
                new Rating {Reviewer = 1, Movie = 3, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {Reviewer = 2, Movie = 1, Grade = 3, Date = DateTime.Parse("2020-03-12")},
                new Rating {Reviewer = 2, Movie = 2, Grade = 4, Date = DateTime.Parse("2020-03-12")},

                new Rating {Reviewer = 3, Movie = 2, Grade = 5, Date = DateTime.Parse("2020-03-12")},
                new Rating {Reviewer = 3, Movie = 3, Grade = 3, Date = DateTime.Parse("2020-03-12")},
            };

            mock.Setup(m => m.GetAll()).Returns(() => returnValue);

            var movieService = new MovieService(mock.Object);

            var expectedResult = new List<int>() { 2, 3, 1 };

            //Act
            var actualResult = movieService.GetTopMoviesByReviewer(reviewer: 1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult),
                "The list of movies in order of rating (subsequently date) is NOT as expected.");

            //RADO DIFFERENT
            Assert.IsTrue(actualResult.ElementAt(0) == 2);
            Assert.IsTrue(actualResult.ElementAt(1) == 3);
            Assert.IsTrue(actualResult.ElementAt(2) == 1);
        }

        [TestMethod]
        public void GetReviewersByMovie_ThrowsArgumentException()
        {
            //Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            var movieService = new MovieService(mock.Object);
            const int invalidInput = 0;

            //Act+Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.GetTopMoviesByReviewer(invalidInput));
        }

        [TestMethod]
        public void GetReviewersByMovie_ReturnsCorrectResults()
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

            var movieService = new MovieService(mock.Object);

            var expectedResult = new List<int>() { 112, 111 };

            //Act
            var actualResult = movieService.GetReviewersByMovie(movie: 1);

            //Assert
            mock.Verify(m => m.GetAll(), Times.Once);

            Assert.IsTrue(actualResult.SequenceEqual(expectedResult),
                "The list of reviewers in order of rating (subsequently date) is NOT as expected.");
        }
    }
}
