using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwindWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NorthwindWebApp.Controllers
{
    public class NorthwindController : Controller
    {
        private readonly NorthwindContext _context;

        public Employee Employees { get; private set; }

        public NorthwindController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Employees = _context.Employees.ToList();
            return View(Employees);
        }

        public IActionResult DeleteEmployee(int id)
        {
            //TO DO add code for detele function
            var foundEmployee = _context.Employees.Find(id);

            if (foundEmployee != null)
            {
               // _context.Orders.
                _context.Employees.Remove(foundEmployee);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }
        


        //[HttpGet]
        //[Route("Northiwnd/EmployeeForm")]
        public IActionResult EmployeeForm(int id)
        {
            if (id == 0)
            {
                return View(new Employee());
            }
            else 
            {
                Employee foundEmployee = _context.Employees.Find(id);
                return View(foundEmployee);
            }
            
        }

        public IActionResult SaveEmployee(Employee newEmployee) 
        {
            if (ModelState.IsValid) 
            {
                if (newEmployee.EmployeeId == 0)
                {
                    _context.Employees.Add(newEmployee);
                    _context.SaveChanges();
                }
                else 
                {
                    Employee employee1 = _context.Employees.Find(newEmployee.EmployeeId);
                    employee1.FirstName = newEmployee.FirstName;
                    employee1.LastName = newEmployee.LastName;

                    _context.Entry(employee1).State = EntityState.Modified;
                    _context.Update(employee1);
                    _context.SaveChanges();


                }
            }

            return RedirectToAction("Index");
        }
    }
}