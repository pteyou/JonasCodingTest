using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Company GetByCode(string companyCode);
        DataEntity SaveCompany(Company company);
        DataEntity DeleteCompany(string company);

        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByCodeAsync(string companyCode);
        Task<DataEntity> SaveCompanyAsync(Company company);
        Task<DataEntity> DeleteCompanyAsync(string company);
    }
}
