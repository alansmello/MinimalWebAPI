using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
  public string Name { get; private set; }
  public bool Active { get; private set; }

  public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();             
    }

  public void Validate()
  {
     var contract = new Contract<Category>()
          .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório!")
          .IsGreaterOrEqualsThan(Name, 3, "Name", "Nome deve ter no mínimo 3 caracteres!")
          .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
          .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
  }
    public void EditInfo(string name, bool active)
    {
      Active = active;
      Name = name;

      Validate();
    }

   
}