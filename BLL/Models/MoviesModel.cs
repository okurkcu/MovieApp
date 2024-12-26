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
    public class MoviesModel
    {
        public Movie Record { get; set; }

        public string Name => Record.Name;

        [DisplayName("Release Date")]
        public string ReleaseDate => Record.ReleaseDate.HasValue ? Record.ReleaseDate.Value.ToString("MM/dd/yyyy") : "Release date is not set";

        //public string TotalRevenue => Record.TotalRevenue.HasValue ? Record.TotalRevenue.Value.ToString("N0") : string.Empty;

        [DisplayName("Total Revenue")]
        [Range(0, double.MaxValue, ErrorMessage = "Total revenue cannot be negative.")]
        public string TotalRevenue => Record.TotalRevenue.HasValue ? Record.TotalRevenue.Value.ToString("N0") : "0";

        public string Director => Record.Director?.Name + " " + Record.Director?.Surname;

        //Can get ID of the genre also!
        public string Genres => string.Join("<br>", Record.MovieGenres?.Select(mg => mg.Genre?.Name));

        //[DisplayName("Genres")]
        //public List<Genre> GenreList => Record.MovieGenres?.Select(mg => mg.Genre).ToList();

        [DisplayName("Genres")]
        public List<int> GenreIds
        {
            get => Record.MovieGenres?.Select(mg => mg.GenreId).ToList();
            set => Record.MovieGenres = value.Select(v => new MovieGenre() { GenreId = v}).ToList();
        }
    }
}
