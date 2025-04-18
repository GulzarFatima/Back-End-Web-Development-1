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

     
        /// <summary> ADD
        /// Adds a new teacher using form data submitted via POST.
        /// </summary>
        /// <param name="TeacherFname">Teacher's first name</param>
        /// <param name="TeacherLname">Teacher's last name</param>
        /// <param name="EmployeeID">Employee number</param>
        /// <param name="HireDate">Date the teacher was hired</param>
        /// <param name="Salary">Teacher's salary (as a string to convert to double)</param>
        /// <returns>
        /// Redirects to the List view after successful addition
        /// </returns>

        [HttpPost]
        public IActionResult AddATeacher(string TeacherFname,String TeacherLname,String EmployeeID,DateTime HireDate,string Salary)

        {
            // Initiative: Check if name fields are empty
            if (string.IsNullOrWhiteSpace(TeacherFname) || string.IsNullOrWhiteSpace(TeacherLname))
            {
                ViewBag.Error = "First name and last name can not be empty.";
                return View("New");
            }

            // Continue if validation passed
         
            Teacher teacher = new Teacher();
            teacher.TeacherFirstName = TeacherFname;
            teacher.TeacherLastName = TeacherLname;
            teacher.EmployeeID = EmployeeID;
            teacher.HireDate = HireDate;
            teacher.Salary = Convert.ToDouble(Salary);

            _api.addATeacher(teacher);
            return RedirectToAction("List");

        }

        /// <summary> DELETE
        /// Deletes a teacher based on the provided ID.
        /// </summary>
        /// <param name="ID">teacher ID to delete</param>
        /// <returns>
        /// Redirects to the teacher list page after deletion.
        /// </returns>
        [HttpPost]
        [Route(template: "/DeleteIsConfirmed/{ID}")]
        public IActionResult Delete(int ID)
        {
            _api.DeleteTeacher(ID);

            return RedirectToAction("List");
        }

        /// <summary> DELETE CONFIRIMATION
        /// Displays a confirmation prompt before deleting a teacher.
        /// </summary>
        /// <param name="ID">The ID of the teacher to delete.</param>
        /// <returns>
        /// A view showing the teacher's information and asking for deletion confirmation.
        /// </returns>

        [HttpGet]
        [Route(template: "/TeacherPage/DeleteCon/{ID}")]
        public IActionResult DeleteConfirmResult(int ID)
        {

            Teacher teacher = _api.FindTeacher(ID);
            return View(teacher);
        }

        /// <summary>
        /// Returns the form view for adding a new teacher
        /// </summary>
        /// <returns>
        /// A View for entering new teacher information
        /// </returns>

        [HttpGet]
        [Route(template: "/New")]

        public IActionResult New()
        {
            return View();
        }

    }
}
