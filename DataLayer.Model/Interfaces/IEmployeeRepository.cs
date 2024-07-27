using DataAccessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByCodeAsync(string employeeCode);
        Task<DataEntity> SaveEmployeeAsync(Employee employee);
        Task<DataEntity> DeleteEmployeeAsync(string employeeCode);
    }
}
