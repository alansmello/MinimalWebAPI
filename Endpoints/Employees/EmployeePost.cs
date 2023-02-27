using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var newUser  = new IdentityUser
        {
          UserName = employeeRequest.email,
          Email = employeeRequest.email
        };
        var result = userManager.CreateAsync(newUser, employeeRequest.password).Result;
        if(!result.Succeeded)
          return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var userClains = new List<Claim>
        {
          new Claim("EmployeeCode", employeeRequest.employeeCode ),
          new Claim("Name", employeeRequest.name),
          new Claim("CreatedBy", userId )
        };
    
        var claimResult = userManager.AddClaimsAsync(newUser, userClains).Result;  
        if(!claimResult.Succeeded)
          return  Results.BadRequest(claimResult.Errors.First());

        

        return Results.Created($"/emplyees/{newUser.Id}", newUser.Id);
    }
}