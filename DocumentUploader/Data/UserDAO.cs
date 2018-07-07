using DocumentUploader.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentUploader.Data
{
    public class UserDAO : IDAO<User>
    {

        AppDbContext _ctx;

        public UserDAO(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(User obj)
        {
            throw new NotImplementedException();
        }

        public User Find(string id)
        {
            return _ctx.Users.FirstOrDefault(u => u.Email == id);
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(User obj)
        {
            throw new NotImplementedException();
        }

        public void Update(User obj)
        {
            _ctx.Attach(obj).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public List<Models.File> GetFiles(string email)
        {
            return _ctx.Files.Where(f => f.Email.Equals(email)).ToList();
        }

        public List<User> GetUsersFromAccount(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}
