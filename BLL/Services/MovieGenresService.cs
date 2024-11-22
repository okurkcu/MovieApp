using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{

    public interface IMovieGenresService
    {
        public IQueryable<MovieGenresModel> Query();

        public ServiceBase Create(MovieGenre record);

        public ServiceBase Update(MovieGenre record);

        public ServiceBase Delete(int id);
    }
    public class MovieGenresService : ServiceBase, IMovieGenresService
    {
        public MovieGenresService(Db db) : base(db)
        {
        }

        public IQueryable<MovieGenresModel> Query()
        {
            //return _db.MovieGenres.OrderBy(m => m.Movie.Name).Select(m => new MovieGenresModel() { Record = m });
            return _db.MovieGenres.Include(mg => mg.Movie).Include(mg => mg.Genre).Select(mg => new MovieGenresModel { Record = mg });
        }

        public ServiceBase Create(MovieGenre record)
        {
            if (record == null)
                return Error("The Movie Genre cannot be null.");

            /*if (_db.MovieGenres.Any(m => m.Movie.Name == record.Movie.Name && m.Genre.Name == record.Genre.Name))
                return Error("This movie already has this genre!");*/

            var movieExist = _db.Movies.Any(m => m.Id == record.MovieId);
            if (!movieExist)
                return Error("Movie cannot be found!");

            var genreExist = _db.Genres.Any(g => g.Id == record.GenreId);
            if (!genreExist)
                return Error("Genre cannot be found!");

            if (_db.MovieGenres.Any(mg => mg.MovieId == record.MovieId && mg.GenreId == record.GenreId))
                return Error("This relation with movie and genre already exist!");

            _db.MovieGenres.Add(record);
            _db.SaveChanges();
            return Success("Movie Genre relation created successfully!");
        }

        public ServiceBase Update(MovieGenre record)
        {
            /*if (_db.MovieGenres.Any(m => m.Movie.Name == record.Movie.Name && m.Genre.Name == record.Genre.Name))
                return Error("This movie already has this genre!");

            record.Genre.Name = record.Movie.Name;
            _db.MovieGenres.Update(record);
            _db.SaveChanges();
            return Success("Movie Genre updated successfully!");*/

            if (record == null)
                return Error("The Movie Genre cannot be null.");

            var movieExist = _db.Movies.Any(m => m.Id == record.MovieId);
            if (!movieExist)
                return Error("Movie cannot be found!");

            var genreExist = _db.Genres.Any(g => g.Id == record.GenreId);
            if (!genreExist)
                return Error("Genre cannot be found!");

            var mgRelation = _db.MovieGenres.SingleOrDefault(mg => mg.Id == record.Id);
            mgRelation.MovieId = record.MovieId;
            mgRelation.GenreId = record.GenreId;

            _db.MovieGenres.Update(mgRelation);
            _db.SaveChanges();
            return Success("Movie Genre relation updated successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.MovieGenres.Include(m => m.Movie).SingleOrDefault(m => m.Id == id);

            _db.MovieGenres.Remove(entity);
            _db.SaveChanges();
            return Success("Movie Genre deleted successfully!");
        }
    }
}
