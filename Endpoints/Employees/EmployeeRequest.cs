using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Endpoints.Employees;

public record EmployeeRequest(string email, string password, string name);