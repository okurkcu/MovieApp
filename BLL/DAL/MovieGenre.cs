﻿using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class MovieGenre
    {
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }
        
        public int GenreId { get; set; }

        //[Required]
        public Movie Movie { get; set; }

        //[Required]
        public Genre Genre { get; set; }
    }
}