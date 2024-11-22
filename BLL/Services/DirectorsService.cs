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
    public interface IDirectorService
    {
        public IQueryable<DirectorsModel> Query();

        public ServiceBase Create(Director record);

        public ServiceBase Update(Director record);

        public ServiceBase Delete(int id);
    }
    public class DirectorsService : ServiceBase, IDirectorService
    {
        public DirectorsService(Db db) : base(db)
        {
        }

        public IQueryable<DirectorsModel> Query()
        {
            return _db.Directors.Include(d => d.Movies).OrderByDescending(d => d.Name).ThenByDescending(d => d.Surname).Select(d => new DirectorsModel() { Record = d });
        }

        public ServiceBase Create(Director record)
        {
            if (_db.Directors.Any(d => d.Name.ToLower() == record.Name.ToLower().Trim() && d.Surname.ToLower() == record.Surname.ToLower().Trim() && d.isRetired == record.isRetired))
                return Error("Director with the same name, surname and retirement status!");

            record.Name = record.Name.Trim();
            _db.Directors.Add(record);
            _db.SaveChanges();
            return Success("Director created successfully!");
        }

        public ServiceBase Update(Director record)
        {
            if (_db.Directors.Any(d => d.Name.ToLower() == record.Name.ToLower().Trim() && d.Surname.ToLower() == record.Surname.ToLower().Trim() && d.isRetired == record.isRetired))
                return Error("Director with the same name, surname and retirement status!");

            var entity = _db.Directors.Find(record.Id);

            if (entity == null)
                return Error("Director cannot be found!");

            entity.Name = record.Name.Trim();
            entity.Surname = record.Surname.Trim();
            entity.isRetired = record.isRetired;
            _db.Directors.Update(entity);
            _db.SaveChanges();
            return Success("Director updated successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.Include(d => d.Movies).SingleOrDefault(d => d.Id == id);

            if (entity == null)
                return Error("Director can't be found!");

            if (entity.Movies.Any())
                return Error("Director has relational movies!");

            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return Success("Director deleted successfully!");
        }
    }
}
