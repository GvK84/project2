using ClassLibraryEmp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static ClassLibraryEmp.Repos;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace ConsoleEmp
{
    class Program
    {
        static void Main(string[] args)
        {
            Repos rep = new Repos();

            StreamReader r1 = new StreamReader("C:\\Users\\gvkap\\source\\repos\\ConsoleEmp\\ConsoleEmp\\positions.json");
            string jsonString1 = r1.ReadToEnd();
            var jsonpositions = JsonConvert.DeserializeObject<List<Position>>(jsonString1);
            foreach(Position pos in jsonpositions)
            {
                if (rep.GetAllPositions().Any(p => string.Equals(p.Name, pos.Name, StringComparison.CurrentCultureIgnoreCase))== false)
                {
                    rep.AddPosition(pos);
                }
            }

            StreamReader r2 = new StreamReader("C:\\Users\\gvkap\\source\\repos\\ConsoleEmp\\ConsoleEmp\\departments.json");
            string jsonString2 = r2.ReadToEnd();
            var jsondepartments = JsonConvert.DeserializeObject<List<Department>>(jsonString2);
            foreach (Department dep in jsondepartments)
            {
                if (rep.GetAllDepartments().Any(d => string.Equals(d.Name,dep.Name, StringComparison.CurrentCultureIgnoreCase))==false)
                {
                    rep.AddDepartment(dep);
                }
            }

            StreamReader r3 = new StreamReader("C:\\Users\\gvkap\\source\\repos\\ConsoleEmp\\ConsoleEmp\\projects.json");
            string jsonString3 = r3.ReadToEnd();
            var jsonprojects = JsonConvert.DeserializeObject<List<Project>>(jsonString3);
            foreach (Project pro in jsonprojects)
            {
                if ((rep.GetAllProjects().Any(j => j.ProjectName == pro.ProjectName)) == false)
                {
                    rep.AddProject(pro);
                }
            }

            StreamReader r4 = new StreamReader("C:\\Users\\gvkap\\source\\repos\\ConsoleEmp\\ConsoleEmp\\employees.json");
            string jsonString4 = r4.ReadToEnd();
            var jsonemployees = JsonConvert.DeserializeObject<List<JsonEmployee>>(jsonString4);
            foreach (JsonEmployee emp in jsonemployees)
            {
                Employee demp = rep.TransformEmp(emp);
                bool compare = rep.GetAllEmployees().Any(e => (e.Name == demp.Name && e.Surname == demp.Surname && e.Salary == demp.Salary && e.StartDate == demp.StartDate && e.PositionId == demp.PositionId && e.DepartmentId == demp.DepartmentId));
                if ( compare == false)
                {
                    rep.AddEmployee(demp);
                }
            }

            IEnumerable<Employee> employees = rep.GetAllEmployees();
            Console.WriteLine("***************************************************************");
            int allcount = employees.Count();
            Console.WriteLine("*** There are currently " + allcount + " employees in the database ***");
            int oldemp = rep.GetOldEmployees();
            int curremp = allcount - oldemp;
            Console.WriteLine("*** There are " + curremp + " current employee(s) and " + oldemp + " former employee(s) ***");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("**** Current Employees ****");
            foreach(Employee emp in employees)
            {
                if (emp.EndDate <= DateTime.MinValue)
                {
                    Console.WriteLine("************************************");
                    Console.WriteLine("   Name: " + emp.Name);
                    Console.WriteLine("   Surname: " + emp.Surname);
                    Console.WriteLine("   Salary: " + emp.Salary+" EUR");
                    Console.WriteLine("   Position: " + rep.GetPosbyID(emp.PositionId));
                    Console.WriteLine("   Department: " + rep.GetDepbyID(emp.DepartmentId));
                    Console.WriteLine("   Projects: " + rep.GetProjectsByEmp(emp.Id));
                    Console.WriteLine("************************************");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Total Salaries: " + rep.GetTotalEmployeeSalaries() + " EUR");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("**** Former Employees ****");
            foreach (Employee emp in employees)
            {
                if (emp.EndDate > DateTime.MinValue)
                {
                    Console.WriteLine("************************************");
                    Console.WriteLine("   Name: " + emp.Name);
                    Console.WriteLine("   Surname: " + emp.Surname);
                    Console.WriteLine("   Salary: " + emp.Salary + " EUR");
                    Console.WriteLine("   Position: " + rep.GetPosbyID(emp.PositionId));
                    Console.WriteLine("   Department: " + rep.GetDepbyID(emp.DepartmentId));
                    Console.WriteLine("   Projects: " + rep.GetProjectsByEmp(emp.Id));
                    Console.WriteLine("************************************");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }
    }
}
