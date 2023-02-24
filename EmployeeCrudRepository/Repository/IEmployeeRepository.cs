using EmployeeCrudRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrudRepository.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get_Employees();
        Employee Get_EmployeeByID(Guid id);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Guid id);
        void Save();
    }
}
