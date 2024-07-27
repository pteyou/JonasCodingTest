using System.Collections.Generic;
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
    }
}
