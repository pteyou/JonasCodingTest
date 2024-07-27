using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using BusinessLayer.Services;
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

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await _employeeService.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        public async Task<EmployeeDto> Get(string employeeCode)
        {
            var items = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeDto>(items);
        }

        public async Task<BaseDto> Post([FromBody] EmployeeDto employee)
        {
            var res = await _employeeService.CreateEmployeeAsync(_mapper.Map<EmployeeInfo>(employee));
            return _mapper.Map<BaseDto>(res);
        }

        public async Task<BaseDto> Put(string employeeCode, [FromBody]EmployeeDto employee)
        {
            var item = await _employeeService.UpdateEmployeeAsync(employeeCode, _mapper.Map<EmployeeInfo>(employee));
            return _mapper.Map<BaseDto>(item);
        }

        public async Task<BaseDto> Delete(string employeeCode)
        {
            var item = await _employeeService.DeleteEmployeeAsync(employeeCode);
            return _mapper.Map<BaseDto>(item);
        }
    }
}