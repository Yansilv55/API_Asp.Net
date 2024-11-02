using API_Coffee.Endpoints.Employees;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API_Coffee.Infra.Data
{
    public class QueryAllUsersWithClaimName
    {
        private readonly IConfiguration configuration;

        public QueryAllUsersWithClaimName(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<EmployeeResponse> Execute(int page, int rows)
        {
            var db = new SqlConnection(configuration["ConnectionString:CoffeeDb"]);
            var query = @"SELECT Email, ClaimValue as Name
                  from AspNetUsers u INNER
                  JOIN AspNetRoleClaims c
                  on u.id = c.UserId and ClaimType = 'Name'
                  order by name
                  OFFSET (@page -1 ) * @rows ROWS FETCH nest @rows ROWS ONLY ";
            return db.Query<EmployeeResponse>(
                query,
                new { page, rows }
                );
        }
    }
}
