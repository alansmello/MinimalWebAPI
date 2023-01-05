using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
   
  public Category(string name)
    {
        var contract = new Contract<Category>()
        .IsNotNullOrEmpty(name, "Name", "Nome é obrigatório!");
        AddNotifications(contract);

        Name = name;
        Active = true;
    }

    public string Name { get; set; }
    public bool Active { get; set; }

   
}