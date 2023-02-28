namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    public readonly IConfiguration Configuration;
    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
            this.Configuration = configuration;
        
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(Configuration["ConnectionStrings:IWantDb"]);
          
      //Dapper
        var query = @"SELECT Email, ClaimValue as Name FROM AspNetUsers u INNER JOIN AspNetUserClaims c ON u.Id = c.UserId and Claimtype = 'Name' order by Name OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY ;";

        return await db.QueryAsync<EmployeeResponse>( query, new {page, rows});
    }
}