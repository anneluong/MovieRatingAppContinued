using MovieRatingApp.Core.DomainService;
using MovieRatingApp.Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MovieRatingApp.Infrastructure.Static.Data.Repositories
{
    public class MovieRepositoryFileReader : IMovieRepository
    {
        private readonly string _path = "ratings.json";

        public MovieRepositoryFileReader()
        {
            GetReviewsFromFile(_path);
        }

        private IEnumerable<Rating> _ratingCollection;

        public IEnumerable<Rating> GetAll()
        {
            return _ratingCollection;
        }

        public void GetReviewsFromFile(string _path)
        {
            using (StreamReader streamReader = File.OpenText(_path))
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                reader.CloseInput = true;
                var serializer = new JsonSerializer();
                var ratings = new List<Rating>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        Rating review = serializer.Deserialize<Rating>(reader);
                        ratings.Add(review);
                    }

                }
                _ratingCollection = ratings;
            }
        }
    }
}
