using DocumentUploader.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentUploader.Data
{
    public class DAO : IDAO<Account>
    {
        private readonly AppDbContext _ctx;

        public DAO(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(Account acc)
        {
            _ctx.Accounts.Add(acc);
            _ctx.SaveChanges();
        }

        public Account Find(string id)
        {
            return _ctx.Accounts.FirstOrDefault(a => a.AccountId == id);
            
        }

        public List<Account> GetAll()
        {
            return _ctx.Accounts.AsNoTracking().ToList();
        }

        public void Remove(Account acc)
        {
            throw new NotImplementedException();
        }

        public void Update(Account acc)
        {
            _ctx.Attach(acc).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public List<Models.File> GetFiles(string email) 
        {
            return _ctx.Files.Where(f => f.Email.Equals(email)).ToList();
        }

        public List<User> GetUsersFromAccount(string accountId)
        {
            return _ctx.Users.Where(u => u.AccountId.Equals(accountId)).ToList();
        }
    }
}
