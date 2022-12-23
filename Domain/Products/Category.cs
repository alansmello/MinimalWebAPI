using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; set; }
    public bool Active { get; set; } = true;
    
}