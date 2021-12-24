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
        // получаем контекст данных через механизм внедрения зависимостей (usersContext добавили как сервис)
        readonly UsersContext uc;
        public HomeController(UsersContext usersContext) 
        {
            uc = usersContext;
        }

        // Авторизация/Проверка Usera в БД
        [HttpGet] // GET-версия, которая отдает представление с формой ввода
        public IActionResult Login() // отображение формы ввода
        {
            return View();
        }      
        [HttpPost] // POST-версия, которая принимает введенные в эту форму данные
        public IActionResult Login(string login, string password) // проверка логина/пароля в БД и выдача соответствущего сообшения
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
        // Регистрация/Добавление нового User в БД
        [HttpGet]// GET-версия, которая отдает представление с формой добавления
        public IActionResult Registration()
        {
            return View();
        }        
        [HttpPost] // POST-версия, которая принимает введенные в эту форму данные
        public IActionResult Registration(string login, string password) // проверка логина на существование
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
