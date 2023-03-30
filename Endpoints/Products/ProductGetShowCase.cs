using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Endpoints.Products;
[AllowAnonymous]
public class ProductGetShowCase
{
    public static string Template => "products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(ApplicationDbContext context)
    {
        var products = context.Products.Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active)
            .OrderBy(p => p.Name).ToList();

        var results = products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active));

        return Results.Ok(results);
    }


}