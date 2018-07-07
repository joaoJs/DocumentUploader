using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocumentUploader.Data;
using DocumentUploader.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using DocumentUploader.Util;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentUploader.Controllers
{
    public class FilesController : Controller
    {

        #region Private Fields
        private readonly IDAO<Account> _dao;
        private readonly IDAO<User> _userDao;
        #endregion

        #region Constructor
        public FilesController(IDAO<Account> dao, IDAO<User> userDao)
        {
            _dao = dao;
            _userDao = userDao;
        }
        #endregion
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        #region FileUploadGet
        [HttpGet]
        public IActionResult FileUpload()
        {
            return View();
        }
        #endregion

        #region FileUploadPost
        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            string[] arr = file.FileName.Split(new Char[] { '\\' });
            string name = arr[arr.Length - 1];

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        "UploadedFiles", name);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Models.File newFile = new Models.File();

            newFile.Name = name;

            //Add file to current user and update user and account     
            //_user.Files.Add(newFile);
            User sessionUser = HttpContext.Session.GetObject<User>("user");
            sessionUser.Files.Add(newFile);
            _userDao.Update(sessionUser);

            return Library();
        }
        #endregion

        #region Download
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", "UploadedFiles", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        #endregion

        #region Library
        public IActionResult Library()
        {
            User sessionUser = HttpContext.Session.GetObject<User>("user");
            Account sessionAccount = HttpContext.Session.GetObject<Account>("account");
            //List<Models.File> files = new List<Models.File>();
            List<Models.File> files = new List<Models.File>();
            files = _userDao.GetFiles(sessionUser.Email);

            //sessionAccount.Users.ForEach(u => files.AddRange(u.Files));

            ViewData["Message"] = files.First().Name;

            return View(files);
        }
        #endregion

        #region LibraryAccount
        public IActionResult LibraryAccount()
        {

            Account sessionAccount = HttpContext.Session.GetObject<Account>("account");
            List<User> users = new List<User>();
            users = _dao.GetUsersFromAccount(sessionAccount.AccountId);
            List<Models.File> files = new List<Models.File>();
            foreach(User u in users)
            {
                files.AddRange(_userDao.GetFiles(u.Email));
            }

            ViewData["Message"] = sessionAccount.AccountId;

            return View(files);
        }
        #endregion

        #region GetContentType
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        #endregion
    }
}
