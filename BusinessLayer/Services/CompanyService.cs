using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public BaseInfo CreateCompany(CompanyInfo companyInfo)
        {
            var res = _companyRepository.SaveCompany(_mapper.Map<Company>(companyInfo));
            return _mapper.Map<BaseInfo>(res);
        }

        public BaseInfo DeleteCompany(string companyCode)
        {
            var res = _companyRepository.DeleteCompany(companyCode);
            return _mapper.Map<BaseInfo>(res);
        }

        public IEnumerable<CompanyInfo> GetAllCompanies()
        {
            var res = _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            var result = _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public BaseInfo UpdateCompany(string companyCode, CompanyInfo companyInfo)
        {
            var res = new DataEntity
            {
                SiteId = string.Empty,
                CompanyCode = string.Empty
            };
            if (string.IsNullOrEmpty(companyInfo.CompanyCode) || companyCode == companyInfo.CompanyCode)
            {
                res = _companyRepository.SaveCompany(_mapper.Map<Company>(companyInfo));
            }
            return _mapper.Map<BaseInfo>(res);
        }
    }
}
