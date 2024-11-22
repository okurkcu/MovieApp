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
    public interface IUserService
    {
        public IQueryable<UsersModel> Query();

        public ServiceBase Create(User record);

        public ServiceBase Update(User record);

        public ServiceBase Delete(int id);
    }
    public class UsersService : ServiceBase, IUserService
    {
        public UsersService(Db db) : base(db)
        {
        }

        public IQueryable<UsersModel> Query()
        {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.UserName).ThenByDescending(u => u.IsActive).Select(u => new UsersModel() { Record = u });
        }

        public ServiceBase Create(User record)
        {
            if (_db.Users.Any(u => u.UserName.ToLower() == record.UserName.ToLower().Trim() && u.IsActive == record.IsActive && u.RoleId == record.RoleId))
                return Error("User with the same name, role and active status exist!");

            record.UserName = record.UserName.Trim();
            _db.Users.Add(record);
            _db.SaveChanges();
            return Success("User created successfully!");
        }

        public ServiceBase Update(User record)
        {
            if (_db.Users.Any(u => u.UserName.ToLower() == record.UserName.ToLower().Trim() && u.IsActive == record.IsActive && u.RoleId == record.RoleId))
                return Error("User with the same name, role and active status exist!");

            var entity = _db.Users.Find(record.Id);

            if (entity == null)
                return Error("User cannot be found!");

            entity.UserName = record.UserName.Trim();
            entity.Password = record.Password;
            entity.IsActive = record.IsActive;
            entity.RoleId = record.RoleId;
            _db.Users.Update(entity);
            _db.SaveChanges();
            return Success("User updated successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Users.Include(u => u.Role).SingleOrDefault(u => u.Id == id);

            if (entity == null)
                return Error("User cannot be found!");

            _db.Users.Remove(entity);
            _db.SaveChanges();
            return Success("User deleted successfully!");
        }
    }
}
