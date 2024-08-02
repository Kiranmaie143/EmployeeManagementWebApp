using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeWebApp.Data;
using EmployeeWebApp.Models;
using EmployeeWebApp.Business;
using EmployeeWebApp.DTO;

namespace EmployeeWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employeeDtos = await _employeeService.GetAllEmployeesAsync();
            return View(employeeDtos);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDto = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employeeDto == null)
            {
                return NotFound();
            }

            return View(employeeDto);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,Department")] EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployeeAsync(employeeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDto);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDto = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employeeDto == null)
            {
                return NotFound();
            }

            return View(employeeDto);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,Department")] EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.UpdateEmployeeAsync(employeeDto);
                }
                catch
                {
                    if (!await EmployeeExists(employeeDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDto);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDto = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employeeDto == null)
            {
                return NotFound();
            }

            return View(employeeDto);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EmployeeExists(int id)
        {
            var employeeDto = await _employeeService.GetEmployeeByIdAsync(id);
            return employeeDto != null;
        }
    }
}
