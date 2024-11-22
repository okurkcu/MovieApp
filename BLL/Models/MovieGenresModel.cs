using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class MovieGenresModel
    {
        public MovieGenre Record { get; set; }

        [Display(Name  = "Movie")]
        public string MovieName => Record.Movie.Name;

        [Display(Name = "Genre")]
        public string GenreName => Record.Genre.Name;

        public int MovieId => Record.MovieId;
        public int GenreId => Record.GenreId;
    }
}
