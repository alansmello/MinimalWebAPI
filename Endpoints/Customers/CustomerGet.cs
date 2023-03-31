using IWantApp.Domain.Users;
using IWantApp.Endpoints.Customers;

namespace IWantApp.Endpoints.Customers;

public class CustomerGet
{
    public static string Template => "/customers";
    public static string[] Methods => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(HttpContext http)
    {
        var user = http.User;

        var result = new
        {
                Id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                Name = user.Claims.First(c => c.Type == "Name").Value,
                CPF = user.Claims.First(c => c.Type == "Cpf").Value,
        };

        return Results.Ok(result);

    }
}