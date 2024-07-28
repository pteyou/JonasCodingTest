using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper, ILogger logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                _logger.LogInformation("GetAll Company endpoint fired");
                var items = await _companyService.GetAllCompaniesAsync();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            try
            {
                _logger.LogInformation("Get Company endpoint fired");
                var item = await _companyService.GetCompanyByCodeAsync(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        // POST api/<controller>
        public async Task<BaseDto> Post([FromBody]CompanyDto company)
        {
            try
            {
                _logger.LogInformation("Create Company endpoint fired");
                var item = await _companyService.CreateCompanyAsync(_mapper.Map<CompanyInfo>(company));
                return _mapper.Map<BaseDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        // PUT api/<controller>/5
        public async Task<BaseDto> Put(string companyCode, [FromBody]CompanyDto company)
        {
            try
            {
                _logger.LogInformation("Update Company endpoint fired");
                var item = await _companyService.UpdateCompanyAsync(companyCode, _mapper.Map<CompanyInfo>(company));
                return _mapper.Map<BaseDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        // DELETE api/<controller>/5
        public async Task<BaseDto> Delete(string companyCode)
        {
            try
            {
                _logger.LogInformation("Delete Company endpoint fired");
                var item = await _companyService.DeleteCompanyAsync(companyCode);
                return _mapper.Map<BaseDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }
    }
}