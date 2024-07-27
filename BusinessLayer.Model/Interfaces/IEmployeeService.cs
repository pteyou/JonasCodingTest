using BusinessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync();
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode);
        Task<BaseInfo> CreateEmployeeAsync(EmployeeInfo employeeInfo);
        Task<BaseInfo> UpdateEmployeeAsync(string employeeCode, EmployeeInfo employeeInfo);
        Task<BaseInfo> DeleteEmployeeAsync(string employeeCode);
    }
}
