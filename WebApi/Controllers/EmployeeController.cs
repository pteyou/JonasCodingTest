using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using BusinessLayer.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                _logger.LogInformation("GetAll Employee endpoint fired");
                var items = await _employeeService.GetAllEmployeesAsync();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public async Task<EmployeeDto> Get(string employeeCode)
        {
            try
            {
                _logger.LogInformation("Get Employee endpoint fired");
                var items = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
                return _mapper.Map<EmployeeDto>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public async Task<BaseDto> Post([FromBody] EmployeeDto employee)
        {
            try
            {
                _logger.LogInformation("Create Employee endpoint fired");
                var res = await _employeeService.CreateEmployeeAsync(_mapper.Map<EmployeeInfo>(employee));
                return _mapper.Map<BaseDto>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public async Task<BaseDto> Put(string employeeCode, [FromBody]EmployeeDto employee)
        {
            try
            {
                _logger.LogInformation("Update Employee endpoint fired");
                var item = await _employeeService.UpdateEmployeeAsync(employeeCode, _mapper.Map<EmployeeInfo>(employee));
                return _mapper.Map<BaseDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public async Task<BaseDto> Delete(string employeeCode)
        {
            try
            {
                _logger.LogInformation("Delete Employee endpoint fired");
                var item = await _employeeService.DeleteEmployeeAsync(employeeCode);
                return _mapper.Map<BaseDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }
    }
}