using System.Security.Claims;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;
    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(CategoryRequest categoryRequest, HttpContext http, ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;// esta consulta é no token e não no banco
        var category = new Category(categoryRequest.Name, userId, userId);
        
        if(!category.IsValid)
        {
         
          return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }
        context.Categories.Add(category);
        context.SaveChanges();
               
        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}