using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Endpoints.Products;

public record ProductRequest(string Name, Guid CategoryId, string Description, bool HasStock, decimal Price, bool Active);
