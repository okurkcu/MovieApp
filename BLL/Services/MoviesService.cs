using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IMoviesService
    {
        public IQueryable<MoviesModel> Query();

        public ServiceBase Create(Movie record);

        public ServiceBase Update(Movie record);

        public ServiceBase Delete(int id);
    }
    public class MoviesService : ServiceBase, IMoviesService
    {
        public MoviesService(Db db) : base(db)
        {
        }

        public IQueryable<MoviesModel> Query()
        {
            return _db.Movies.Include(p => p.Director).Include(m =>  m.MovieGenres).ThenInclude(mg => mg.Genre).OrderByDescending(m => m.Name).ThenByDescending(m => m.ReleaseDate).ThenByDescending(m => m.Director.Name).Select(m => new MoviesModel() { Record = m });
        }

        public ServiceBase Create(Movie record)
        {
            if (_db.Movies.Any(m => m.Name.ToLower() == record.Name.ToLower().Trim() && m.DirectorId == record.DirectorId && m.ReleaseDate == record.ReleaseDate))
                return Error("Movie with the same name, director and release date exist!");

            if (record.TotalRevenue == null)
                record.TotalRevenue = 0;

            record.Name = record.Name.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges();
            return Success("Movie is added successfully!");
        }

        public ServiceBase Update(Movie record)
        {
            if (_db.Movies.Any(m => m.Name.ToLower() == record.Name.ToLower().Trim() && m.Director == record.Director && m.ReleaseDate == record.ReleaseDate))
                return Error("Movie with the same name, director and release date exist!");

            //var entity = _db.Movies.Find(record.Id);
            var entity = _db.Movies.Include(mg => mg.MovieGenres).SingleOrDefault(m => m.Id == record.Id);

            if (entity == null)
                return Error("Movie cannot be found!");

            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            entity.Name = record.Name.Trim();
            entity.ReleaseDate = record.ReleaseDate;
            entity.TotalRevenue = record.TotalRevenue;
            entity.Director = record.Director;
            entity.MovieGenres = record.MovieGenres;
            _db.Movies.Update(entity);
            _db.SaveChanges();
            return Success("Movie updated successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies.Include(m => m.MovieGenres).SingleOrDefault(m => m.Id == id);

            if (entity == null)
                return Error("Movie cannot be found!");

            //if (entity.MovieGenres.Any())
            //    return Error("Movie has relational Movie Genres!");

            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return Success("Movie has deleted successfully!");
        }
    }
}
