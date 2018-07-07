using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentUploader.Models;

namespace DocumentUploader.Data
{
    public interface IDAO<T>
    {

        void Add(T obj);
        T Find(string id);
        List<T> GetAll();
        void Remove(T obj);
        void Update(T obj);
        List<File> GetFiles(string id);
        List<User> GetUsersFromAccount(string accountId);
    }
}
