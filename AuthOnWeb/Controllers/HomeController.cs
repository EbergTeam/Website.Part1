using AuthOnWeb.Entity;
using AuthOnWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AuthOnWeb.Controllers
{
    public class HomeController : Controller
    {
        readonly UsersContext uc;
        public HomeController(UsersContext usersContext)
        {
            uc = usersContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            if (uc.Users.Any(u => u.Login == login && u.Password == password))
            {
                return Content("Добро пожаловать, " + login);
            }
            else
            {
                return Content("Ошибка авторизации");
            }
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(string login, string password)
        {
            if (uc.Users.Any(u => u.Login == login))
            {
                return Content("Пользователь с такии логином уже существует");
            }
            else
            {
                uc.Add(new User() { Login = login, Password = password });
                uc.SaveChanges();
                return Content("Пользователь добавлен");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
