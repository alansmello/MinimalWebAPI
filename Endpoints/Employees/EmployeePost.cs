using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user  = new IdentityUser
        {
          UserName = employeeRequest.email,
          Email = employeeRequest.email
        };
        var result = userManager.CreateAsync(user, employeeRequest.password).Result;
        if(!result.Succeeded)
          return Results.BadRequest(result.Errors.First());

        var userClains = new List<Claim>
        {
          new Claim("EmployeeCode", employeeRequest.employeeCode ),
          new Claim("Name", employeeRequest.name)
        };
    
        var claimResult = userManager.AddClaimsAsync(user, userClains).Result;  
        if(!claimResult.Succeeded)
          return  Results.BadRequest(claimResult.Errors.First());

        

        return Results.Created($"/emplyees/{user.Id}", user.Id);
    }
}