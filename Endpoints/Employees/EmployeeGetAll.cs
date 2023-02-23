using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
     /* var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();  
      var employees = new List<EmployeeResponse>();
      foreach (var item in users)
      {
        var claims  = userManager.GetClaimsAsync(item).Result;
        var claimName = claims.FirstOrDefault(c=> c.Type == "Name");
        var userName = claimName != null ? claimName.Value : string.Empty;
        employees.Add(new EmployeeResponse(item.Email, userName));
      }

      return Results.Ok(employees);*/

      
        if(page == null)
            return Results.BadRequest("O numero da página deve ser informado.");
        if(rows == null) 
            return Results.BadRequest("O numero de linhas deve ser informado.");
        if(rows > 10) 
            return Results.BadRequest("O numero de linhas não pode ser maior que 10.");   

        var employees = query.Execute(page.Value,rows.Value);
        
        return Results.Ok(employees);
    }
}