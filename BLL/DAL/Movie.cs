using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Movie
    {
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        public decimal? TotalRevenue { get; set; }

        public int DirectorId { get; set; }

        [Required]
        public Director Director { get; set; }
        public List<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
