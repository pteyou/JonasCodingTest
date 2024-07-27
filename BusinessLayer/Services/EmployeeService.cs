using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }
        public async Task<BaseInfo> CreateEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var res = await _employeeRepository.SaveEmployeeAsync(_mapper.Map<Employee>(employeeInfo));
            return _mapper.Map<BaseInfo>(res);
        }

        public async Task<BaseInfo> DeleteEmployeeAsync(string employeeCode)
        {
            var res = await _employeeRepository.DeleteEmployeeAsync(employeeCode);
            return _mapper.Map<BaseInfo>(res);
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var res = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            var result = await _employeeRepository.GetByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task<BaseInfo> UpdateEmployeeAsync(string employeeCode, EmployeeInfo employeeInfo)
        {
            var res = new DataEntity
            {
                SiteId = string.Empty,
                CompanyCode = string.Empty
            };
            if (string.IsNullOrEmpty(employeeInfo.EmployeeCode) || employeeCode == employeeInfo.EmployeeCode)
            {
                res = await _employeeRepository.SaveEmployeeAsync(_mapper.Map<Employee>(employeeInfo));
            }
            return _mapper.Map<BaseInfo>(res);
        }
    }
}
