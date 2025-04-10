using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cumulative1.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TeacherAPIController : ControllerBase
    {


        // Private connection to the database
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        //API Set up - GET request to the endpoint /api/Teacher
        [HttpGet(template: "Teacher")]

        //this is the method that will call the database and return a list of teachers.


        /// <summary>
        /// Retrieves a list of all Teachers from the database.
        /// </summary>
        /// 
        /// <example>
        /// GET api/Teacher -> [{"TeacherId":1,"FirstName":"Sarah","LastName":"Josh",...},..]
        /// </example>
        /// 
        /// <returns>
        /// List of Teacher objects containing ID, Name, EmployeeID, HireDate, and Salary
        /// </returns>



        public List<Teacher> ListTeacherNames()
        {
            // Initialize a list to store teacher objects retrieved from the database.
            List<Teacher> teachers = new List<Teacher>();


            // Establish a connection to the database using the context.
            MySqlConnection Connection = _context.GetConnection();


            // Open the database connection.
            Connection.Open();

            Debug.WriteLine("DbConnected");

            // SQL query to get all teachers from the database.
            string SQLQuery = "SELECT * FROM teachers";


            // Create a command to run the SQL query.
            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = SQLQuery;

            // Run the query and get the data.
            MySqlDataReader DataReader = Command.ExecuteReader();


            // Go through each row of data.
            while (DataReader.Read())
            {
                // Get the teacher data from the row.
                int TeacherId = Convert.ToInt32(DataReader["teacherid"]);
                string TeacherFName = DataReader["teacherfname"].ToString();
                string TeacherLName = DataReader["teacherlname"].ToString();
                string EmployeeID = DataReader["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(DataReader["hiredate"]);
                double Salary = Convert.ToDouble(DataReader["salary"]);

                // Create a new teacher object and set its data.
                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFirstName = TeacherFName;
                newTeacher.TeacherLastName = TeacherLName;
                newTeacher.EmployeeID = EmployeeID;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;

                // Add the new teacher to the list.
                teachers.Add(newTeacher);


            }
            // Close the connection to the database.
            Connection.Close();


            // Return the list of teachers.
            return teachers;
        }



        /// <summary>
        /// Gets a teacher by their ID from the database.
        /// </summary>
        /// <example>
        /// GET api/FindTeacher/1 -> {"TeacherId":1,"TeacherFirstName":"Sarah","TeacherLastName":"Josh",...}
        /// </example>
        /// <param name="id">The ID of the teacher to get.</param>
        /// <returns>
        /// A Teacher object with ID, Name, EmployeeID, HireDate, and Salary, or an empty object if not found.
        /// </returns>


        [HttpGet]
        [Route(template: "FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            // Create a teacher object to store the result.
            Teacher teacher = new Teacher();

            // Connect to the database.
            MySqlConnection Connection = _context.GetConnection();

            // Open the connection.
            Connection.Open();

            // SQL query to find a teacher by ID.
            string SQL = "Select * FROM teachers Where Teacherid = " + id.ToString();

            // Create a command to run the SQL query.
            MySqlCommand Command = Connection.CreateCommand();
            Command.CommandText = SQL;

            // Run the query and get the data.
            MySqlDataReader DataReader = Command.ExecuteReader();

            // Get the data for the teacher.
            while (DataReader.Read())
            {
                // Set the teacher's data.
                int TeacherId = Convert.ToInt32(DataReader["teacherid"]);
                string TeacherFName = DataReader["teacherfname"].ToString();
                string TeacherLName = DataReader["teacherlname"].ToString();
                string EmployeeID = DataReader["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(DataReader["hiredate"]);
                double Salary = Convert.ToDouble(DataReader["salary"]);

                teacher.TeacherId = TeacherId;
                teacher.TeacherFirstName = TeacherFName;
                teacher.TeacherLastName = TeacherLName;
                teacher.EmployeeID = EmployeeID;
                teacher.HireDate = HireDate;
                teacher.Salary = Salary;
            }

            // Close the database connection.
            Connection.Close();

            // Return the teacher object.
            return teacher;
        }

        /// <summary>
        /// Deletes a teacher from the database by ID.
        /// </summary>
        /// <param name="TeacherId">The ID of the teacher to delete.</param>
        /// <example>
        /// DELETE api/DeleteTeacher/1
        /// </example>
        /// <returns>
        /// Number of rows affected by the delete operation.
        /// </returns>
        [HttpDelete]
        [Route("DeleteTeacher/{TeacherId}")]
        public int DeleteTeacher(int TeacherId)
        {
            // Connect to the database.
            MySqlConnection Connection = _context.GetConnection();

            // Open the connection.
            Connection.Open();

            // Create a SQL DELETE command to remove the teacher by ID.
            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = "DELETE FROM teachers WHERE teacherid = @id";

            // Add the parameter to prevent SQL injection.
            Command.Parameters.AddWithValue("@id", TeacherId);

            // Execute the delete command and store the number of rows affected.
            int RowsAffected = Command.ExecuteNonQuery();

            return RowsAffected;
        }

    }
}
