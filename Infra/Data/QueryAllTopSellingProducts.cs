using IWantApp.Endpoints.Report;

namespace IWantApp.Infra.Data;

public class QueryAllTopSellingProducts
{
    public readonly IConfiguration Configuration;
    public QueryAllTopSellingProducts(IConfiguration configuration)
    {
            this.Configuration = configuration;
        
    }

    public async Task<IEnumerable<ProductResponseReport>> Execute()
    {
        var db = new SqlConnection(Configuration["ConnectionStrings:IWantDb"]);
          
      //Dapper
        var query = @"SELECT p.Id, p.Name, p.Price,  COUNT(*) AS QuantidadeVendida
                       FROM OrderProdutcs op
                       INNER JOIN Products p ON op.ProductsId = p.Id
                       GROUP BY p.Id, p.Name, p.Price
                       ORDER BY QuantidadeVendida DESC;";

        return await db.QueryAsync<ProductResponseReport>(query);
    }
}