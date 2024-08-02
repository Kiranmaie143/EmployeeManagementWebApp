using AutoMapper;
using EmployeeWebApp.DataAccess;
using EmployeeWebApp.DTO;
using EmployeeWebApp.Models;

namespace EmployeeWebApp.Business
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDataAccess _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeDataAccess employeeRepository, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllEmployeesAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                return _mapper.Map<EmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeByIdAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                await _employeeRepository.AddEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddEmployeeAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                await _employeeRepository.UpdateEmployeeAsync(employee);
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
                await _employeeRepository.DeleteEmployeeAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteEmployeeAsync)}: {ex.Message}");
                throw;
            }
        }
    }
}
