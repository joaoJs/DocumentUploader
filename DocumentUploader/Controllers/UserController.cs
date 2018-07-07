using DocumentUploader.Data;
using DocumentUploader.Models;
using DocumentUploader.Util;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentUploader.Controllers
{
    public class UserController : Controller
    {

        #region Private Fields
        private readonly IDAO<Account> _dao;
        private readonly IDAO<User> _userDao;
        #endregion

        #region Constructor
        public UserController(IDAO<Account> dao, IDAO<User> userDao)
        {
            _dao = dao;
            _userDao = userDao;
        }
        #endregion

        #region RegisterUser
        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            if (!String.IsNullOrEmpty(user.AccountId))
            {
                CheckProvidedId(user);
                return View("User");
            }
            else
            {

                Account newAccount = new Account();
                User newUser = new User();

                SetNewUser(user, newUser);
                SetNewAccountId(newUser, newAccount);
                newAccount.Users.Add(newUser);
                _dao.Add(newAccount);

                ViewData["Message"] = "Signin successfull and account created.";

                HttpContext.Session.SetObject("account", newAccount);
                HttpContext.Session.SetObject("user", newUser);

                return View("User", newUser);
            }
        }
        #endregion

        #region LogUser
        [HttpPost]
        public IActionResult LogUser(User user)
        {
            User userFromDb = _userDao.Find(user.Email);
            if (userFromDb != null)
            {
                if (userFromDb.Password.Equals(user.Password))
                {
                    Account accFromDb = _dao.Find(userFromDb.AccountId);
                    ViewData["Message"] = "Login succesfull!";

                    HttpContext.Session.SetObject("account", accFromDb);
                    HttpContext.Session.SetObject("user", userFromDb);
                    return View("User", userFromDb);
                }
                else
                {
                    ViewData["Message"] = "Invalid Password.";
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                ViewData["Message"] = "Forgot your email?";
                return View("~/Views/Home/Login.cshtml");
            }
        }
        #endregion

        #region Confirm
        public IActionResult Confirm(string fname, string path)
        {

            ViewData["Message"] = "The filename is: " + fname +
                " and the path is: " + path;

            return View();
        }
        #endregion

        #region Log
        public IActionResult Log(string message)
        {
            ViewData["message"] = message;

            return View();
        }
        #endregion

        #region UserAction
        public IActionResult UserAction(User user)
        {
            ViewData["Message"] = user.Email;

            return View("User", user);
        }
        #endregion

        #region CheckProvidedId
        public IActionResult CheckProvidedId(User user)
        {
            Account accFromDb = _dao.Find(user.AccountId);
            if (accFromDb != null)
            {
                accFromDb.Users.Add(user);
                _dao.Update(accFromDb);

                ViewData["Message"] = "Sign in successfull! You joined an existing account.";

                HttpContext.Session.SetObject("account", accFromDb);
                HttpContext.Session.SetObject("user", user);

                return View("user", user);
            }
            else
            {
                ViewData["Message"] = "Invalid Account Id";
                return View("~/Views/Home/Signin.cshtml");
            }
        }
        #endregion

        #region SetNewAccountId

        public void SetNewAccountId(User user, Account newAccount)
        {
            List<Account> accountsFromDb = _dao.GetAll();
            if (string.IsNullOrEmpty(user.AccountId))
            {
                string nextId;
                if (accountsFromDb.Count > 0)
                {
                    int id = Int32.Parse(accountsFromDb.Last<Account>().AccountId);
                    nextId = (id + 1).ToString();
                }
                else
                {
                    nextId = "0";
                }
                newAccount.AccountId = nextId;
                user.AccountId = nextId;
            }
            else
            {
                
                newAccount.AccountId = user.AccountId;
            }

        }

        #endregion

        #region SetNewUser
        public void SetNewUser(User user, User newUser)
        {
            newUser.AccountId = user.AccountId;
            newUser.Email = user.Email;
            newUser.Password = user.Password;
        }
        #endregion

    }
}