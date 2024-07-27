using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;

	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public DataEntity DeleteCompany(string companyCode)
        {
            var result = new DataEntity
            {
                SiteId = string.Empty,
                CompanyCode = string.Empty,
            };
            if(_companyDbWrapper.Delete(c => c.CompanyCode == companyCode))
            {
                result = new DataEntity
                {
                    CompanyCode = companyCode
                };
            }
            return result;
        }

        public IEnumerable<Company> GetAll()
        {
            return _companyDbWrapper.FindAll();
        }

        public Company GetByCode(string companyCode)
        {
            return _companyDbWrapper.Find(t => t.CompanyCode.Equals(companyCode))?.FirstOrDefault();
        }

        public DataEntity SaveCompany(Company company)
        {
            var result = new DataEntity
            {
                SiteId = string.Empty,
                CompanyCode = string.Empty,
            };

            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                if(_companyDbWrapper.Update(itemRepo))
                {
                    result = new DataEntity
                    {
                        SiteId = company.SiteId,
                        CompanyCode = company.CompanyCode,
                    };
                }
            }

            else if(_companyDbWrapper.Insert(company))
            {
                result = new DataEntity
                {
                    SiteId = company.SiteId,
                    CompanyCode = company.CompanyCode,
                };
            }
            return result;
        }
    }
}
