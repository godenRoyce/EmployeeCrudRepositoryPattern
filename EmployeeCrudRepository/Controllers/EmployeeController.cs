using EmployeeCrudRepository.Models;
using EmployeeCrudRepository.Repository;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeCrudRepository.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employee;
        public EmployeeController()
        {
            _employee = new EmployeeRepository(new EmployeeContext());
        }
        public EmployeeController(IEmployeeRepository employee)
        {
            _employee = employee;
        }
        // GET: Employee
        public ActionResult Index()
        {
            var employee = _employee.Get_Employees();
            return View(employee);
        }
        public ActionResult Details(Guid id) 
        { 
            if (id == Guid.Empty || id == null)
            {
                return View("Error");
            }
            var employee = _employee.Get_EmployeeByID(id);
            return View(employee);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee) 
        { 
            if (ModelState.IsValid) 
            {
                _employee.AddEmployee(employee);
                _employee.Save();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult Edit(Guid id)
        {
            var employee = _employee.Get_EmployeeByID(id);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _employee.UpdateEmployee(employee);
                _employee.Save();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult Delete(Guid id)
        {
            var employee = _employee.Get_EmployeeByID(id);
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _employee.DeleteEmployee(id);
            _employee.Save();
            return RedirectToAction("Index");
        }

        public ActionResult PDF(Guid id)
        {
            var employee = _employee.Get_EmployeeByID(id);
            if (employee != null)
            {
                String path = "C:\\Users\\royce\\Desktop\\TamimiMarketsExam\\EmployeeCrudRepository\\EmployeeCrudRepository\\App_Data\\employee-details.pdf";
                PdfWriter writer = new PdfWriter(path);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                Paragraph header = new Paragraph("Employee Details").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(20);
                document.Add(header);

                Table table = new Table(2, true);
                Cell name_label = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .Add(new Paragraph("Name"));
                Cell name_value = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .Add(new Paragraph(employee.Name));
                Cell email_label = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Email"));
                Cell email_value = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .Add(new Paragraph(employee.Email));
                Cell age_label = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Age"));
                Cell age_value = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .Add(new Paragraph(employee.Age.ToString()));
                Cell job_label = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Job Position"));
                Cell job_value = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .Add(new Paragraph(employee.jobPosition));
                Cell city_label = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .Add(new Paragraph("City"));
                Cell city_value = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .Add(new Paragraph(employee.city));

                table.AddCell(name_label);
                table.AddCell(name_value);
                table.AddCell(email_label);
                table.AddCell(email_value);
                table.AddCell(age_label);
                table.AddCell(age_value);
                table.AddCell(job_label);
                table.AddCell(job_value);
                table.AddCell(city_label);
                table.AddCell(city_value);


                document.Add(table);

                document.Close();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}