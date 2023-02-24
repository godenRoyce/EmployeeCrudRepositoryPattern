using EmployeeCrudRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Table = iText.Layout.Element.Table;

namespace EmployeeCrudRepository.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly EmployeeContext _dbcontext;

        public EmployeeRepository()
        {
            _dbcontext = new EmployeeContext();
        }
        public EmployeeRepository(EmployeeContext dbcontext) 
        { 
            _dbcontext = dbcontext;
        }
        public void AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _dbcontext.Employees.Add(employee);
            Save();
        }

        public void DeleteEmployee(Guid id)
        {
            var employee = _dbcontext.Employees.Find(id);
            if (employee != null)
            {
                _dbcontext.Employees.Remove(employee);
            }
        }

        public Employee Get_EmployeeByID(Guid id)
        {
            return _dbcontext.Employees.Find(id);
        }

        public IEnumerable<Employee> Get_Employees()
        {
            return _dbcontext.Employees.ToList();
        }

        public void Save()
        {
            _dbcontext.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _dbcontext.Entry(employee).State = EntityState.Modified;
        }
        private bool _dispose = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this._dispose)
            {
                if (disposing)
                {
                    _dbcontext.Dispose();
                }               
            }
            this._dispose = true;
        }

        void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}