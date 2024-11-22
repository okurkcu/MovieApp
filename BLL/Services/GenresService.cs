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
    public interface IGenreService
    {
        public IQueryable<GenresModel> Query();

        public ServiceBase Create(Genre record);

        public ServiceBase Update(Genre record);

        public ServiceBase Delete(int id);
    }
    public class GenresService : ServiceBase, IGenreService
    {
        public GenresService(Db db) : base(db) 
        {
        }

        public IQueryable<GenresModel> Query()
        {
            return _db.Genres.Include(g => g.MovieGenres).OrderByDescending(g => g.Name).Select(g => new GenresModel { Record = g });
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(g => g.Name.ToLower() == record.Name.ToLower().Trim()))
                return Error("Genre with the same name exist!");

            record.Name = record.Name.Trim();
            _db.Genres.Add(record);
            _db.SaveChanges();
            return Success("Genre created successfully!");
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(g => g.Name.ToLower() == record.Name.ToLower().Trim()))
                return Error("Genre with the same name exist!");

            var entity = _db.Genres.Find(record.Id);

            if (entity == null)
                return Error("Genre cannot be found!");

            entity.Name = record.Name.Trim();
            _db.Genres.Update(entity);
            _db.SaveChanges();
            return Success("Genre created successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(g => g.MovieGenres).SingleOrDefault(g => g.Id == id);

            if (entity == null)
                return Error("Genre cannot be found!");

            if (entity.MovieGenres.Any())
                return Error("Genre has relational Movie Genres!");

            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully!");
        }
    }

    
}
