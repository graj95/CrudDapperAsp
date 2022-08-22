using CrudDapperAsp.Models;

namespace CrudDapperAsp.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<int> CreateCompany(CompanyForCreationDto company);
        public Task<int> UpdateCompany(CompanyForUpdateDto company);
        public Task<int> DeleteCompany(int id);
    }
}
