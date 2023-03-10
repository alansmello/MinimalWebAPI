namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var newUser  = new IdentityUser
        {
          UserName = employeeRequest.email,
          Email = employeeRequest.email
        };
        var result = await userManager.CreateAsync(newUser, employeeRequest.password);

        if(!result.Succeeded)
          return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var userClains = new List<Claim>
        {
          new Claim("EmployeeCode", employeeRequest.employeeCode ),
          new Claim("Name", employeeRequest.name),
          new Claim("CreatedBy", userId )
        };
    
        var claimResult = await userManager.AddClaimsAsync(newUser, userClains);  

        if(!claimResult.Succeeded)
          return  Results.BadRequest(claimResult.Errors.First());

        return Results.Created($"/emplyees/{newUser.Id}", newUser.Id);
    }
}