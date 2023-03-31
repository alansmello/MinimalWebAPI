using IWantApp.Domain.Users;
using IWantApp.Endpoints.Customers;

namespace IWantApp.Endpoints.Customers;

public class CustomerPost
{
    public static string Template => "/customers";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(CustomerRequest customerRequest,  UsersCreator userCreator)
    {
       var userClaims = new List<Claim>
       {
          new Claim ("Cpf", customerRequest.CPF),
          new Claim ("Name", customerRequest.Name)
       };
       
      (IdentityResult identity, string userId ) result = await userCreator.Create(customerRequest.Email, customerRequest.Password, userClaims);

      if(!result.identity.Succeeded)
        return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/customers/{result.userId}", result.userId);
    }
}