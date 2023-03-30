using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Endpoints.Products;

public class ProductGetId
{
    public static string Template => "products/{id:Guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString()};
    public static Delegate Handle => Action; 

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action( [FromRoute] Guid id, ApplicationDbContext context )
    {
     
        var product = await context.Products.Where(p => p.Id == id).FirstAsync();
        if(product == null)
            return Results.NotFound();

        var category = await context.Categories.Where(p => p.Id == product.CategoryId).FirstAsync();
        if(category == null)
            return Results.NotFound();

        var productResponse = new ProductResponse(product.Name, category.Name ,product.Description, product.HasStock, product.Price, product.Active );

        return Results.Ok(productResponse);
    }
}