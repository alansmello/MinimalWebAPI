namespace IWantApp.Domain.Products;

public class Product : Entity
{

    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public bool HasStock { get; private set; }
    public bool Active { get; private set; } = true;   

    
    public Product()
    {
    }

    public Product(string name, Category category, string description, bool hasStock, string createBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;

        CreatedBy = createBy;
        EditedBy = createBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
     
    }

    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category", "Category not found")
            .IsGreaterOrEqualsThan(Description, 3, "Description")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);

    }
}