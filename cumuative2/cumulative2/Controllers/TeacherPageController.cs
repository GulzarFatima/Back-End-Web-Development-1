using cumulative1.Models;
using Microsoft.AspNetCore.Mvc;


namespace cumulative1.Controllers
{
    // This controller handles requests from the teacher page.
    public class TeacherPageController : Controller
    {

        // Stores the connection to the API controller.
        private readonly TeacherAPIController _api;


        // Sets up the connection to the API controller.
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }



        /// <summary>
        /// Gets a list of all teachers to show on the page.
        /// </summary>
        /// <example>
        /// GET TeacherPage/List -> [{"TeacherId":1,"TeacherFirstName":"Sarah","TeacherLastName":"Josh",...},..]
        /// </example>
        /// <returns>
        /// List of teachers with their ID, name, employee ID, hire date, and salary.
        /// </returns>

        public IActionResult List()
        {

            // Get the list of teachers from the API.
            List<Teacher> Teach = _api.ListTeacherNames();
            // return the list of teachers on the page.
            return View(Teach);
        }



        /// <summary>
        /// Gets details of a teacher by their ID.
        /// </summary>
        /// <example>
        /// GET TeacherPage/Show/1 -> {"TeacherId":1,"TeacherFirstName":"Sarah","TeacherLastName":"Josh",...}
        /// </example>
        /// <param name="Id">The ID of the teacher.</param>
        /// <returns>
        /// A teacher's details with their ID, name, employee ID, hire date, and salary.
        /// </returns>

        public IActionResult Show(int Id)
        {
            // Get the teacher's details from the API by ID.
            Teacher teach1 = _api.FindTeacher(Id);

            // Returns teacher's details on the page.
            return View(teach1);
        }
    }
}
