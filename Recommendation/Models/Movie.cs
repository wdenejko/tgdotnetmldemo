﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Recommendation.Models
{
    class Movie
    {
        public int movieId;

        public string movieTitle;
        private static readonly string BaseDatasetsLocation = @"../../../Data";

        public Lazy<List<Movie>> _movies = new Lazy<List<Movie>>(() => LoadMovieData($"{BaseDatasetsLocation}/recommendation-movies.csv"));

        public Movie()
        {
        }

        public Movie Get(int id)
        {
            return _movies.Value.Single(m => m.movieId == id);
        }

        private static List<Movie> LoadMovieData(string moviesdatasetpath)
        {
            var result = new List<Movie>();
            Stream fileReader = File.OpenRead(moviesdatasetpath);
            StreamReader reader = new StreamReader(fileReader);
            try
            {
                bool header = true;
                int index = 0;
                var line = "";
                while (!reader.EndOfStream)
                {
                    if (header)
                    {
                        line = reader.ReadLine();
                        header = false;
                    }
                    line = reader.ReadLine();
                    string[] fields = line.Split(',');
                    int movieId = Int32.Parse(fields[0].ToString().TrimStart(new char[] { '0' }));
                    string movieTitle = fields[1].ToString();
                    result.Add(new Movie() { movieId = movieId, movieTitle = movieTitle });
                    index++;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return result;
        }
    }
}
