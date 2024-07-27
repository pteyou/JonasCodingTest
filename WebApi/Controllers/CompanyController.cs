using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public IEnumerable<CompanyDto> GetAll()
        {
            var items = _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public CompanyDto Get(string companyCode)
        {
            var item = _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public BaseDto Post([FromBody]CompanyDto company)
        {
            var item = _companyService.CreateCompany(_mapper.Map<CompanyInfo>(company));
            return _mapper.Map<BaseDto>(item);
        }

        // PUT api/<controller>/5
        public BaseDto Put(string companyCode, [FromBody]CompanyDto company)
        {
            var item = _companyService.UpdateCompany(companyCode, _mapper.Map<CompanyInfo>(company));
            return _mapper.Map<BaseDto>(item);
        }

        // DELETE api/<controller>/5
        public BaseDto Delete(string companyCode)
        {
            var item = _companyService.DeleteCompany(companyCode);
            return _mapper.Map<BaseDto>(item);
        }
    }
}