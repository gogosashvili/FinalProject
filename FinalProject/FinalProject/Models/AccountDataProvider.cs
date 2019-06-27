using FinalProject.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class AccountDataProvider
    {
        BookEntities _db = new BookEntities();

        public IEnumerable<User> GetAllUsers(string name, string surname, string email)
        {
            return _db.Users.Where(e => e.IsAdmin == 0 && e.Name.Contains(name) && e.Surname.Contains(surname) && e.Email.Contains(email));
        }

        public User GetUserById(int userId)
        {
            return _db.Users.FirstOrDefault(e => e.Id == userId);
        }

        public bool existUser(User model)
        {
            return _db.Users.FirstOrDefault(e => e.Email == model.Email) == null ? false : true;
        }

        public void add(User model)
        {
            if (!existUser(model))
            {
                _db.Users.Add(model);
                _db.SaveChanges();
            }
        }

        ////////////gadasakeTebeli/wasashleli
        public void Edit(User model)
        {
            var result = GetUserById(model.Id);

            result.Name = model.Name;
            result.Surname = model.Surname;
            result.Password = model.Password;
            result.PhoneNumber = model.PhoneNumber;

            _db.SaveChanges();
        }

        public void EditAccount(RegisterViewModel model)
        {
            var result = GetUserById(model.Id);

            result.Name = model.Name;
            result.Surname = model.Surname;
            result.Password = model.Password;
            result.PhoneNumber = model.PhoneNumber;

            _db.SaveChanges();
        }

        public void Delete(User model)
        {
            var result = GetUserById(model.Id);

            result.IsActive = 0;

            _db.SaveChanges();
        }

        public bool validLogin(LoginViewModel user)
        {
            //var pass = SHA.GenerateSHA12Stirng(user.Password);
            var pass = user.Password;
            var result = _db.Users.FirstOrDefault(e => e.Email == user.Email && e.Password == pass);

            LoginHelper.CreateUser(result);
            //LoginHelper.CreateUser(new User()
            //{
            //    Id = result.Id,
            //    Password = user.Password,
            //    Email = user.Email
            //});
            return result == null ? false : true;
        }

    }
}