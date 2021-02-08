using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlutterwaveServices.MovieAPI.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MoviesStatsModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int MovieId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int WatchDurationMs { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MoviesStatsReturnModel
    {
        public int movieId { get; set; }
        public string title { get; set; }
        public long averageWatchDurations { get; set; }
        public int watches { get; set; }
        public int releaseYear { get; set; }
    }
}
