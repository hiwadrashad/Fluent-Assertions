using Fluent_Assertions_Database.Models;
using Fluent_Assertions_Library.DTOs;
using Fluent_Assertions_Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Fluent_Assertions_Database.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserRepository<User> _userRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository<User> userREpository)
        {
            _logger = logger;
            _userRepository = userREpository;
        }

        public IActionResult Index()
        {
            _userRepository.Save(new Fluent_Assertions_Library.DTOs.User { Emailadress = "test@hotmail.com", LastName = "kljsdflkjd", Name = "sdfgkhdsa" });
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
