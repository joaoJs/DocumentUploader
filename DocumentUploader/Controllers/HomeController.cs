using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DocumentUploader.Models;
using DocumentUploader.Data;
using DocumentUploader.Util;

namespace DocumentUploader.Controllers
{
    public class HomeController : Controller
    {

        private readonly IDAO<Account> _dao;

        public string Layout { get; private set; }

        public HomeController(IDAO<Account> dao)
        {
            _dao = dao;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<User>("user") != null)
            {
                ViewData["Layout"] = "~/Views/Shared/_Layout.cshtml";
            }
            else
            {
                ViewData["Layout"] = "~/Views/Shared/_LayoutIndex.cshtml";
            }
            return View();
        }

        public IActionResult IndexAfter()
        {

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Signin()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
