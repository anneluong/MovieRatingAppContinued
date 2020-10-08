using MovieRatingApp.Core.DomainService;
using MovieRatingApp.Core.Entities;
using MovieRatingApp.Infrastructure.Static.Data.Repositories;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IMovieRepository rep = new MovieRepositoryFileReader();
            List<Rating> ratings = (List<Rating>)rep.GetAll();
            for (int i = 0; i < 10; i++)
            {
                Rating c = ratings[i];
                Console.WriteLine("Reviewer = " + c.Reviewer + " date = " + c.Date);
            }
        }
    }
}
