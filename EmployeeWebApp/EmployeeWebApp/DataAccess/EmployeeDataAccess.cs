using EmployeeWebApp.Data;
using EmployeeWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeWebApp.DataAccess
{
    public class EmployeeDataAccess :IEmployeeDataAccess
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeDataAccess> _logger;

        public EmployeeDataAccess(ApplicationDbContext context , ILogger<EmployeeDataAccess> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllEmployeesAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await _context.Employees.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeByIdAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddEmployeeAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateEmployeeAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteEmployeeAsync)}: {ex.Message}");
                throw;
            }
        }
    }
}
