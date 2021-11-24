using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;

namespace ClassLibraryEmp
{
    public class Repos : IEmployeeRepository
    {
        public class Employee 
        {
            private int id, positionid, departmentid;
            private double salary;
            private string name, surname;
            private DateTime? startdate, enddate;
            public int Id { get { return id; } set { id = value; } }
            public int PositionId { get { return positionid; } set { positionid = value; } }
            public int DepartmentId { get { return departmentid; } set { departmentid = value; } }
            public double Salary { get { return salary; } set { salary = value; } }
            public string Name { get { return name; } set { name = value; } }
            public string Surname { get { return surname; } set { surname = value; } }
            public DateTime? StartDate { get { return startdate; } set { startdate = value; } }
            public DateTime? EndDate { get { return enddate; } set { enddate = value; } }
        }
        public class Department
        {
            public int id;
            public string name;
            public int Id { get { return id; } set { id = value; } }
            public string Name { get { return name; } set { name = value; } }
        }
        public class Position
        {
            public int id;
            public string name;
            public int Id { get { return id; } set { id = value; } }
            public string Name { get { return name; } set { name = value; } }
        }
        public class Project
        {
            public int id;
            public string projectname;
            public DateTime? startdate, enddate;
            public int Id { get { return id; } set { id = value; } }
            public string ProjectName { get { return projectname; } set { projectname = value; } }
            public DateTime? StartDate { get { return startdate; } set { startdate = value; } }
            public DateTime? EndDate { get { return enddate; } set { enddate = value; } }

        }
        public class ProjectEmployee
        {
            public int projectid, employeeid;
            public int ProjectId { get { return projectid; } set { projectid = value; } }
            public int EmployeeId { get { return employeeid; } set { employeeid = value; } }


        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            List<Employee> emps = new List<Employee>();
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("select * from Employee", sqlconn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var empl = new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                    Surname = Convert.ToString(reader["Surname"]),
                    Salary = Convert.ToDouble(reader["Salary"]),
                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                    EndDate =  (reader["EndDate"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(reader["EndDate"]),
                    PositionId = Convert.ToInt32(reader["PositionId"]),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"])
                };

                emps.Add(empl);
            }
            reader.Close();
            sqlconn.Close();
            var employees = emps as IEnumerable<Employee>;
            return employees;
        }
        public double GetTotalEmployeeSalaries()
        {
            double salaries = 0;
            foreach (Employee emp in GetAllEmployees())
            {
                salaries += emp.Salary;
            }
            return salaries;
        }
        public Employee GetByEmployeeId(int id)
        {
            var emps = GetAllEmployees();
            return emps.First(a => a.Id == id);
        }
        public Employee AddEmployee(Employee employee)
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("insert into Employee (Name, Surname, Salary, StartDate, EndDate, PositionId, DepartmentId) values " +
                "(@Name, @Surname, @Salary, @StartDate, @EndDate, @PositionId, @DepartmentId)",sqlconn);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Surname", employee.Surname);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@StartDate", (employee.StartDate==DateTime.MinValue) ? DBNull.Value : employee.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", (employee.EndDate == DateTime.MinValue) ? DBNull.Value : employee.EndDate);
            cmd.Parameters.AddWithValue("@PositionId", employee.PositionId);
            cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return employee;            
        }
        public Project AddProject(Project project)
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("insert into Projects (ProjectName, StartDate, EndDate) values (@ProjectName, @StartDate, @EndDate)", sqlconn);
            cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
            cmd.Parameters.AddWithValue("@StartDate", project.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", project.EndDate);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return project;
        }
        public void AddProjectEmployee(ProjectEmployee projectEmployee)
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("insert into ProjectEmployee (ProjectId, EmployeeId) values (@ProjectId, @EmployeeId)", sqlconn);
            cmd.Parameters.AddWithValue("@ProjectId", projectEmployee.ProjectId);
            cmd.Parameters.AddWithValue("@EmployeeId", projectEmployee.EmployeeId);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
        }
        public Position AddPosition(Position position)
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("insert into Positions (Name) values (@Name)", sqlconn);
            cmd.Parameters.AddWithValue("@Name", position.Name);  
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return position;
        }
        public Department AddDepartment(Department department)
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("insert into Departments (Name) values (@Name)", sqlconn);
            cmd.Parameters.AddWithValue("@Name", department.Name);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return department;
        }
        public IEnumerable<Position> GetAllPositions()
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            List<Position> poss = new List<Position>();
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("select * from Positions", sqlconn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var pos = new Position
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"])                    
                };

                poss.Add(pos);
            }
            reader.Close();
            sqlconn.Close();
            var positions = poss as IEnumerable<Position>;
            return positions;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            List<Department> deps = new List<Department>();
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("select * from Departments", sqlconn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var dep = new Department
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"])
                };

                deps.Add(dep);
            }
            reader.Close();
            sqlconn.Close();
            var departments = deps as IEnumerable<Department>;
            return departments;
        }
        public IEnumerable<Project> GetAllProjects()
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            List<Project> pros = new List<Project>();
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("select * from Projects", sqlconn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var pro = new Project
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    ProjectName = Convert.ToString(reader["ProjectName"]),
                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                    EndDate = Convert.ToDateTime(reader["EndDate"])
                };

                pros.Add(pro);
            }
            reader.Close();
            sqlconn.Close();
            var projects = pros as IEnumerable<Project>;
            return projects;
        }
        public class JsonEmployee
        {
            private string department, name, surname, salary, position, startdate, enddate;
            public string Department { get { return department; } set { department = value; } }
            public string Name { get { return name; } set { name = value; } }
            public string Surname { get { return surname; } set { surname = value; } }
            public string Salary { get { return salary; } set { salary = value; } }
            public string Position { get { return position; } set { position = value; } }
            public string StartDate { get { return startdate; } set { startdate = value; } }
            public string EndDate { get { return enddate; } set { enddate = value; } }

        }
        public Employee TransformEmp(JsonEmployee json)
        {

            Employee demp = new Employee()
            {
                Name = json.Name,
                Surname = json.Surname,
                Salary = double.Parse(json.Salary, CultureInfo.InvariantCulture),
                DepartmentId = GetDepbyName(json.Department),
                PositionId = GetPosbyName(json.Position),
                StartDate = Convert.ToDateTime(json.StartDate),
                EndDate = Convert.ToDateTime(json.EndDate)
            };
            return demp;
        }
        public int GetDepbyName(string dName)
        {
            int did = GetAllDepartments().Where(a => string.Equals(a.Name, dName, StringComparison.CurrentCultureIgnoreCase)).Select(a => a.Id).Distinct().FirstOrDefault();
            if (did == 0)
            {
                Department newd = new Department() { Name = dName };
                AddDepartment(newd);
                did= GetAllDepartments().Where(a => string.Equals(a.Name, dName, StringComparison.CurrentCultureIgnoreCase)).Select(a => a.Id).Distinct().FirstOrDefault();
            }
            return did;
        }
        public string GetDepbyID(int dID)
        {
            string dname = GetAllDepartments().Where(a => a.Id == dID).Select(a => a.Name).Distinct().FirstOrDefault();
            return dname;
        }
        public int GetPosbyName(string pName)
        {
            int pid = GetAllPositions().Where(a => string.Equals(a.Name, pName, StringComparison.CurrentCultureIgnoreCase)).Select(a => a.Id).Distinct().FirstOrDefault();
            if (pid == 0)
            {
                Position newp = new Position() { Name = pName };
                AddPosition(newp);
                pid = GetAllPositions().Where(a => string.Equals(a.Name, pName, StringComparison.CurrentCultureIgnoreCase)).Select(a => a.Id).Distinct().FirstOrDefault();
            }
            return pid;
        }
        public string GetPosbyID(int pID)
        {
            string pname = GetAllPositions().Where(a => a.Id == pID).Select(a => a.Name).Distinct().FirstOrDefault();
            return pname;
        }

        public int GetOldEmployees()
        {
            int o = 0;
            foreach (Employee emp in GetAllEmployees())
            {
                if (emp.EndDate > DateTime.MinValue)
                {
                    o+=1;
                }
            }
            return o;
        }
        public string GetProjectsByEmp(int empid)
        {
            string connString = "Server = DESKTOP-531GO5H; Database = EMPLOYEES; User Id = sa; Password = admin1!;";
            SqlConnection sqlconn = new SqlConnection(connString);
            List<string> projs = new List<string>();
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("select a.ProjectId, b.ProjectName from ProjectEmployee a inner join Projects b on a.ProjectId = b.id where a.EmployeeId = @empid", sqlconn);
            cmd.Parameters.AddWithValue("@empid", empid);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                projs.Add(Convert.ToString(reader["ProjectName"]));
            }
            reader.Close();
            sqlconn.Close();
            string projects = string.Join(",", projs);
            return projects;

        }



    }
}
