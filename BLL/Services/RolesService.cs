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
    public interface IRoleService
    {
        public IQueryable<RoleModel> Query();
        public ServiceBase Create(Role record);
        public ServiceBase Update(Role record);
        public ServiceBase Delete(int id);
    }
    public class RolesService : ServiceBase, IRoleService
    {
        public RolesService(Db db) : base(db)
        {
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(r => r.Name).Select(r => new RoleModel() { Record = r });
        }

        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(r => r.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Roles with the same name exist!");

            record.Name = record.Name?.Trim();
            _db.Roles.Add(record);
            _db.SaveChanges();
            return Success("Role is created successfully!");
        }

        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(r => r.Id != record.Id && r.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Roles with the same name exist!");

            var entity = _db.Roles.Find(record.Id);

            if (entity == null)
                return Error("Role cannot be found!");

            entity.Name = record.Name?.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Role is updated successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);

            if (entity == null)
                return Error("Role cannot be found!");

            if (entity.Users.Any())
                return Error("Role has relational users!");

            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully!");
        }
    }
}
