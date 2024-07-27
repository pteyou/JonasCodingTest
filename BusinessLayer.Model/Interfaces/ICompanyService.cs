using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        BaseInfo CreateCompany(CompanyInfo companyInfo);
        BaseInfo UpdateCompany(string companyCode, CompanyInfo companyInfo);
        BaseInfo DeleteCompany(string companyCode);

        Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync();
        Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode);
        Task<BaseInfo> CreateCompanyAsync(CompanyInfo companyInfo);
        Task<BaseInfo> UpdateCompanyAsync(string companyCode, CompanyInfo companyInfo);
        Task<BaseInfo> DeleteCompanyAsync(string companyCode);
    }
}
