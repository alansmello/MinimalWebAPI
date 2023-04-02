using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantApp.Domain.Orders;

namespace IWantApp.Endpoints.Orders;


public class OrderPost
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http, ApplicationDbContext context)
    {
        /*if(orderRequest.ProductIds == null || !orderRequest.ProductIds.Any())
            return Results.BadRequest("Produto é obrigatório");
        if(string.IsNullOrEmpty(orderRequest.DeliveryAddress))
            return Results.BadRequest("Endereço de Entrega é obrigatório");*/
      
        var customerId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var customerName = http.User.Claims.First(c => c.Type == "Name").Value;

        List<Product> productsFound = null;
        
        if(orderRequest.ProductIds == null || orderRequest.ProductIds.Any())  
            productsFound = context.Products.Where(p => orderRequest.ProductIds.Contains(p.Id)).ToList();
        
         
        var order = new Order(customerId, customerName, productsFound, orderRequest.DeliveryAddress);

    
        if(!order.IsValid)
        {
                    return Results.ValidationProblem(order.Notifications.ConvertToProblemDetails());
        }
        
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        
        return Results.Created($"/orders/{customerId}",customerId );
    }
}
