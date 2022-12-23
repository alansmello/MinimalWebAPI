using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Endpoints.Categories;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
}