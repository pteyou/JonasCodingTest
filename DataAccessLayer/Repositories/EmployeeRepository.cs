using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }
        public async Task<DataEntity> DeleteEmployeeAsync(string employeeCode)
        {
            var result = new DataEntity
            {
                SiteId = string.Empty,
                CompanyCode = string.Empty,
            };
            var employee = await GetByCodeAsync(employeeCode);
            if (await _employeeDbWrapper.DeleteAsync(c => c.EmployeeCode == employeeCode))
            {
                result = new DataEntity
                {
                    SiteId = employee.SiteId,
                    CompanyCode = employee.CompanyCode,
                };
            }
            return result;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            return (await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode)))?.FirstOrDefault(); ;
        }

        public async Task<DataEntity> SaveEmployeeAsync(Employee employee)
        {
            var result = new DataEntity
            {
                SiteId = string.Empty,
                CompanyCode = string.Empty,
            };

            var itemRepo = (await _employeeDbWrapper.FindAsync(t =>
                t.SiteId.Equals(employee.SiteId) && 
                t.CompanyCode.Equals(employee.CompanyCode) &&
                t.EmployeeCode.Equals(employee.EmployeeCode)
                ))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                if (await _employeeDbWrapper.UpdateAsync(itemRepo))
                {
                    result = new DataEntity
                    {
                        SiteId = employee.SiteId,
                        CompanyCode = employee.CompanyCode,
                    };
                }
            }

            else if (await _employeeDbWrapper.InsertAsync(employee))
            {
                result = new DataEntity
                {
                    SiteId = employee.SiteId,
                    CompanyCode = employee.CompanyCode,
                };
            }
            return result;
        }
    }
}
