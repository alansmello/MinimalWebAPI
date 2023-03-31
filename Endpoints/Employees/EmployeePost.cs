using IWantApp.Domain.Users;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UsersCreator userCreator)
    {
        
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        
        var userClains = new List<Claim>
        {
          new Claim("EmployeeCode", employeeRequest.EmployeeCode ),
          new Claim("Name", employeeRequest.Name),
          new Claim("CreatedBy", userId )
        };
      
        (IdentityResult identity, string UserId) result = await userCreator.Create(employeeRequest.Email, employeeRequest.Password, userClains );

        if(!result.identity.Succeeded)
          return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());


        return Results.Created($"/emplyees/{result.UserId}", result.UserId);
    }
}