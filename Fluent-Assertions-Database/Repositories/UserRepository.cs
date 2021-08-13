using Fluent_Assertions_Database.Database;
using Fluent_Assertions_Library.DTOs;
using Fluent_Assertions_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluent_Assertions_Database.Repositories
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbcontext)
        {
            _userDbContext = userDbcontext;
        }

        public bool Validate(T user)
        {


            if (user.Emailadress == "" || user.Emailadress == null)
            {
                throw new NullReferenceException("emptyinput");
            }

            if (user.LastName == "" || user.LastName == null)
            {
                throw new NullReferenceException("emptyinput");
            }

            var emailmatches = _userDbContext.Users.Where(a => a.Emailadress == user.Emailadress);

            if (emailmatches.Count() > 0)
            {
                throw new ArgumentException("wrongvalue");
            }

            if (!(Fluent_Assertions_Library.Methods.Email.EmailIsValid(user.Emailadress)))
            {
                throw new ArgumentException("wrongvalue");
            }

            return true;
        }

        public void OrderEmailAlphabetically()
        {
            _userDbContext.Users.OrderBy(a => a.Emailadress);
        }

        public void Save(T user)
        {

            if (Validate(user))
            {
                _userDbContext.Users.Add(user);
                _userDbContext.SaveChanges();
            }
        }

        public List<T> GetAll()
        {
            if (typeof(T) == typeof(User))
            {
                return new List<T>(_userDbContext.Users.ToList().Cast<T>());
            }
            else
            {
                return null;
            }
        }

        public List<T> SaveAndReturnAll(ref Action<T> Save, ref Func<List<T>> GetAll, T user)
        {
            try
            {
                Save?.Invoke(user);
                var AllUsers = GetAll?.Invoke();
                Save = null;
                GetAll = null;
                return AllUsers;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ArgumentException("wrongvalue");
            }
        }

        public (bool?, List<T>)? VerifyAndReturnAll(ref Func<T, bool> Verify, ref Func<List<T>> GetAll, T user)
        {
            try
            {
                bool? Verified = Verify?.Invoke(user);
                List<T> AllUsers = GetAll?.Invoke();
                Verify = null;
                GetAll = null;
                (bool?, List<T>) ReturnTPL = (Verified, AllUsers);
                return ReturnTPL;

            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return null;
            }
        }
    }
}
