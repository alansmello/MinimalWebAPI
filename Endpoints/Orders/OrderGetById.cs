using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantApp.Domain.Orders;

namespace IWantApp.Endpoints.Orders;

public class OrderGetById
{
    public static string Template => "/orders/{id?}";
    public static string[] Methods => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action([FromRoute] string? id, HttpContext http, ApplicationDbContext context, UserManager<IdentityUser> userManager )
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        
        Order order = null;
        
        if(string.IsNullOrEmpty(id))
            order = await context.Orders.Include(o => o.Products).Where(o => o.CustomerId == userId).FirstAsync();
        else
            order = await context.Orders.Include(o => o.Products).Where(o => o.CustomerId == id).FirstAsync();

        if(order == null)
            return Results.NotFound();
        
        var customer = await userManager.FindByIdAsync(order.CustomerId);

        var orderProduct = order.Products.Select(p => new OrderProduct(p.Id, p.Name, p.Price));

        
        var orderResponse = new OrderResponse(order.CustomerId, customer.Email, orderProduct, order.Total, order.DeliveryAddress);
       

        if(orderResponse == null)
          return Results.NotFound();
        
   

        return Results.Ok(orderResponse);
    }
}