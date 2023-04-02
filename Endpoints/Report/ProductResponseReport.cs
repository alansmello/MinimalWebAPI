using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Endpoints.Report;

public record ProductResponseReport(Guid Id, string Name, decimal Price, int QuantidadeVendida);
