using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Endpoints.Products;

public record ProductResponse(string Name, string CategoryName, string Description, bool HasStock, bool Active);