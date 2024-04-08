using Microsoft.EntityFrameworkCore;
using BanksDB.DataBase;
using BanksDB.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BanksDB
{
    public class Repository
    {
        public IEnumerable<User> GetAllUsers()
        {
            using (var db = new DataBaseContext())
            {
                return db.Users.AsNoTracking().ToList();
            }
        }

        public IEnumerable<CreditType> GetAllCreditTypes()
        {
            using (var db = new DataBaseContext())
            {
                return db.CreditTypes.AsNoTracking().ToList();
            }
        }

        public IEnumerable<Credit> GetAllCredits()
        {
            using (var db = new DataBaseContext())
            {
                return db.Credits.AsNoTracking().ToList();
            }
        }
        public IEnumerable<Credit> GetUserCredit(int userId)
        {
            using (var db = new DataBaseContext())
            {
                return db.Credits.Where(m => m.userId == userId).AsNoTracking().ToList();
            }
        }

        public void AddNewUser(User user)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var f = db.Users.Add(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddNewCreditType(CreditType creditType)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    db.CreditTypes.Add(creditType);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddNewCredit(Credit credit)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    db.Credits.Add(credit);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}