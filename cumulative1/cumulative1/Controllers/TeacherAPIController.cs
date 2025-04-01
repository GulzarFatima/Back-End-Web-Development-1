using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cumulative1.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : Controller
    {
        //Private connection to the database
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/TeacherAPI
        [HttpGet(template: "Teacher")]

        public List<Teacher> ListTeacherNames()
            
        {









            return View();
        }
    }
}
