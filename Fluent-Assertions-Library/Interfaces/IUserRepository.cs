using Fluent_Assertions_Library.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluent_Assertions_Library.Interfaces
{
    public interface IUserRepository<T> where T : User
    {
        bool Validate(T user);
        void Save(T user);
        List<T> GetAll();
        public void OrderEmailAlphabetically();
        public List<T> SaveAndReturnAll(ref Action<T> Save, ref Func<List<T>> GetAll, T user);
        public (bool?, List<T>)? VerifyAndReturnAll(ref Func<T, bool> Verify, ref Func<List<T>> GetAll, T user);
    }
}
