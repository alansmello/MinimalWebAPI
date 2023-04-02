using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantApp.Domain.Orders;

namespace IWantApp.Endpoints.Report;

public class TopSellingProducts
{
    public static string Template => "/top_products";
    public static string[] Methods => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(HttpContext http, QueryAllTopSellingProducts query)
    {
        var employeeCode = http.User.Claims.First(c => c.Type == "EmployeeCode").Value;

        if(employeeCode != "002")
            return Results.Forbid();
        
        /*string sql = @"SELECT p.Id, p.Name, p.Price,  COUNT(*) AS QuantidadeVendida
                       FROM OrderProdutcs op
                       INNER JOIN Products p ON op.ProductsId = p.Id
                       GROUP BY p.Id, p.Name, p.Price
                       ORDER BY QuantidadeVendida DESC;";

        var products = await context.Database.GetDbConnection().QueryAsync<ProductResponseReport>(sql);*/

        var result = await query.Execute();

        return Results.Ok(result);
    }
}