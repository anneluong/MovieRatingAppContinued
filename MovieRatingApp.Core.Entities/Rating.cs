using System;

namespace MovieRatingApp.Core.Entities
{
    public class Rating
    {
        public int ReviewerId { get; set; }
        public int MovieId { get; set; }
        public int Grade { get; set; }
        public DateTime Date { get; set; }
    }
}
