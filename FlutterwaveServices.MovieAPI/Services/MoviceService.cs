using FlutterwaveServices.MovieAPI.Model;
using FlutterwaveServices.MovieAPI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlutterwaveServices.MovieAPI.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MoviceService
    {
        private List<MovieMetaDataModel> movieMetaDataModels;
        private List<MoviesStatsModel> moviesStatsModels;

        /// <summary>
        /// 
        /// </summary>
        public MoviceService()
        {
            movieMetaDataModels = new List<MovieMetaDataModel>();
            moviesStatsModels = new List<MoviesStatsModel>();

            movieMetaDataModels = GetMovieMetaDataModelsFromFile();
            moviesStatsModels = GetMoviesStatsModelFromFile();
        }

        private List<MovieMetaDataModel> GetMovieMetaDataModelsFromFile()
        {
            var fileSavePath = Path.Combine("Data", "metadata.csv");
            if (!File.Exists(fileSavePath))
            {
                return new List<MovieMetaDataModel>();
            }

            var dataRows = FileHelper.GetDataFromFile(fileSavePath);
            if (dataRows.Any())
            {
                var list = new List<MovieMetaDataModel>();
                foreach (var item in dataRows)
                {
                    var element = new MovieMetaDataModel();
                    element.duration = item["duration"].ToString();
                    element.language = item["language"].ToString();
                    element.movieId = int.Parse(item["movieId"].ToString());
                    element.releaseYear = int.Parse(item["releaseYear"].ToString());
                    element.title = item["title"].ToString();
                    list.Add(element);
                }

                return list;
            }
            return new List<MovieMetaDataModel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void CreateMovieMetaDataModel(MovieMetaDataModel model)
        {
            movieMetaDataModels.Add(model);
        }

        private List<MoviesStatsModel> GetMoviesStatsModelFromFile()
        {
            var fileSavePath = Path.Combine("Data", "stats.csv");
            if (!File.Exists(fileSavePath))
            {
                return new List<MoviesStatsModel>();
            }

            var dataRows = FileHelper.GetDataFromFile(fileSavePath);
            if (dataRows.Any())
            {
                var list = new List<MoviesStatsModel>();
                foreach (var item in dataRows)
                {
                    var element = new MoviesStatsModel();
                    element.MovieId = int.Parse(item["movieId"].ToString());
                    element.WatchDurationMs = int.Parse(item["watchDurationMs"].ToString());
                    list.Add(element);
                }

                return list;
            }
            return new List<MoviesStatsModel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MovieMetaDataModel> GetMovieMetaDataModels()
        {
            return movieMetaDataModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public List<MovieMetaDataModel> GetMovieMetaDataModelsByMovieId(int movieId)
        {
            return movieMetaDataModels.Where(c => c.movieId == movieId).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MoviesStatsModel> GetMoviesStatsModels()
        {
            return moviesStatsModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MoviesStatsReturnModel> GetMoviesStatsModelsForStatus()
        {
            var moviesStatsReturnModels = new List<MoviesStatsReturnModel>();
            var groupOfMovies = moviesStatsModels.GroupBy(c => c.MovieId);
            foreach (var item in groupOfMovies)
            {
                var movie = movieMetaDataModels.Where(c => c.movieId == item.Key).FirstOrDefault();
                if (movie != null)
                {
                    var moviesStats = item.ToList();

                    var MoviesStatsReturnModel = new MoviesStatsReturnModel
                    {
                        movieId = movie.movieId,
                        releaseYear = movie.releaseYear,
                        title = movie.title,
                        watches = moviesStats.Count,
                    };

                    long sum = moviesStats.Select(i => (long)i.WatchDurationMs)       
                                          .Aggregate((a, b) => a + b);

                    //long sumOfWatches = moviesStats.Sum(c => c.WatchDurationMs);
                    MoviesStatsReturnModel.averageWatchDurationS = sum / moviesStats.Count;
                    moviesStatsReturnModels.Add(MoviesStatsReturnModel);
                }
            }

            return moviesStatsReturnModels
                .OrderByDescending(c => c.watches)
                .ThenByDescending(c => c.releaseYear)
                .ToList();
        }
    }
}
