using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Endpoints.Customers;

public record CustomerRequest(string Email, string Password, string Name, string CPF);