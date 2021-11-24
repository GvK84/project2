
using System.Collections.Generic;
using static ClassLibraryEmp.Repos;

public interface IEmployeeRepository
    {
        /// <summary>
        /// Should return all employees
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> GetAllEmployees();

        /// <summary>
        /// Should return the sum of all the salaries of employees
        /// </summary>
        /// <returns></returns>
        double GetTotalEmployeeSalaries();

        /// <summary>
        /// Should return a single employee by his id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Employee GetByEmployeeId(int id);

        /// <summary>
        /// Adds the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        Employee AddEmployee(Employee employee);

        /// <summary>
        /// Adds a new project to the database.
        /// </summary>
        /// <param name="project">The project to add to the database.</param>
        /// <returns>The project that was added.</returns>
        Project AddProject(Project project);

        /// <summary>
        /// Adds an employee to  a project.
        /// </summary>
        /// <param name="projectEmployee">The project employee.</param>
        void AddProjectEmployee(ProjectEmployee projectEmployee);

        /// <summary>
        /// Adds a new position to the database.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        Position AddPosition(Position position);

        /// <summary>
        /// Adds a new department to the database.
        /// </summary>
        /// <param name="department">The department to add.</param>
        /// <returns>The new department.</returns>
        Department AddDepartment(Department department);
    }
