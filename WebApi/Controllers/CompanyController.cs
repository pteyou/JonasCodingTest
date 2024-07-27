using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async Task<BaseDto> Post([FromBody]CompanyDto company)
        {
            var item = await _companyService.CreateCompanyAsync(_mapper.Map<CompanyInfo>(company));
            return _mapper.Map<BaseDto>(item);
        }

        // PUT api/<controller>/5
        public async Task<BaseDto> Put(string companyCode, [FromBody]CompanyDto company)
        {
            var item = await _companyService.UpdateCompanyAsync(companyCode, _mapper.Map<CompanyInfo>(company));
            return _mapper.Map<BaseDto>(item);
        }

        // DELETE api/<controller>/5
        public async Task<BaseDto> Delete(string companyCode)
        {
            var item = await _companyService.DeleteCompanyAsync(companyCode);
            return _mapper.Map<BaseDto>(item);
        }
    }
}