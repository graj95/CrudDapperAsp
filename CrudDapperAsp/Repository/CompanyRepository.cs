using CrudDapperAsp.Context;
using CrudDapperAsp.Models;
using CrudDapperAsp.Repository.Interfaces;
using Dapper;

namespace CrudDapperAsp.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCompany(CompanyForCreationDto company)
        {
            int result = 0;
            var query = @"INSERT INTO Companies (Name,Address,Country) 
                          VALUES (@Name,@Address,@Country);
                          SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = _context.CreateConnection())
            {
                result = await connection.QuerySingleAsync<int>(query, company);
                if (result != 0)
                {
                    var result1 = await AddEmployee(company.Employees, result);
                }
                return result;
            }
        }

        private async Task<int> AddEmployee(List<Employee> employees, int CompanyId)
        {
            int result = 0;
            using (var connection = _context.CreateConnection())
            {
                if (employees.Count > 0)
                {
                    foreach (Employee employee in employees)
                    {
                        employee.CompanyId = CompanyId;
                        var query = @"INSERT INTO Employees(Name,Age,Position,CompanyId) 
                          VALUES (@Name,@Age,@Position,@CompanyId)";
                        var result1 = await connection.ExecuteAsync(query, employee);
                        result = result + result1;
                    }
                }
                return result;
            }
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            var query = "SELECT * FROM Companies";
            using (var connection = _context.CreateConnection())
            {
                var companiesraw = await connection.QueryAsync<Company>(query);
                companies = companiesraw.ToList();
                foreach (var company in companies)
                {
                    var EmployeeRow = await connection.QueryAsync<Employee>(@"SELECT * FROM Employees 
                                                                           where CompanyId=@id",
                                                                          new { id = company.Id });
                    company.Employees = EmployeeRow.ToList();
                }
                return companies;
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            Company? company = null;
            var query = "SELECT * FROM Companies where Id=@id";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query, new { id = id });
                company = companies.FirstOrDefault();
                if (company != null)
                {
                    var EmployeeRow = await connection.QueryAsync<Employee>(@"SELECT * FROM Employees 
                                                                           where CompanyId=@id",
                                                                           new { id = id });
                    company.Employees = EmployeeRow.ToList();
                }
                return company;
            }
        }

        public async Task<int> UpdateCompany(CompanyForUpdateDto company)
        {
            int result = 0;
            var query = @"UPDATE Companies SET Name=@Name, Address=@Address,
                          Country=@Country WHERE Id=@Id";

            using (var connection = _context.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, company);
                if (result != 0)
                {
                    result = await connection.ExecuteAsync(@"delete from Employees where CompanyId=@CompanyId"
                                                           , new { CompanyId = company.Id });
                    var result1 = await AddEmployee(company.Employees, company.Id);
                }
                return result;
            }
        }

        public async Task<int> DeleteCompany(int id)
        {
            int result = 0;
            var query = @"Delete from Employees where CompanyId=@id
                          Delete from Companies WHERE Id=@id";
            using (var connection = _context.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, new { id = id });
                return result;
            }
        }
    }

       
}
