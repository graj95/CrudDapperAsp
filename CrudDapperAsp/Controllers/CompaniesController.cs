using CrudDapperAsp.Models;
using CrudDapperAsp.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapperAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;
        public CompaniesController(ICompanyRepository _companyRepo)
        {
            this._companyRepo = _companyRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepo.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCompaniesbyId(int Id)
        {
            try
            {
                var companie = await _companyRepo.GetCompany(Id);
                return Ok(companie);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyForCreationDto company)
        {
            try
            {
                var result = await _companyRepo.CreateCompany(company);

                if (result == 0)
                {
                    return StatusCode(409, "The request could not be processed because of conflict in the request");
                }
                else
                {
                    return StatusCode(200, string.Format("Record Inserted Successfuly with compnay Id {0}", result));
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdateCompany(CompanyForUpdateDto company)
        {
            try
            {
                var result = await _companyRepo.UpdateCompany(company);
                if (result == 0)
                {
                    return StatusCode(409, "The request could not be processed because of conflict in the request");
                }
                else
                {
                    return StatusCode(200, string.Format("Record Updated Successfuly"));
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var result = await _companyRepo.DeleteCompany(id);
                if (result == 0)
                {
                    return StatusCode(409, "The request could not be processed because of conflict in the request");
                }
                else
                {
                    return StatusCode(200, string.Format("Record Deleted Successfuly"));
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



    }
}
