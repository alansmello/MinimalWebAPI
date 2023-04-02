using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWantApp.Domain.Orders;

public class Order : Entity
{
    public string CustomerId{get; private set;}
    public List<Product> Products{get; private set;}

    public decimal Total {get; private set;}

    public string DeliveryAddress{get; private set;}

    public Order(){}

    public Order(string customerId, string customerName, List<Product> products, string deliveryAdress)
    {
        CustomerId = customerId;
        Products = products;
        DeliveryAddress = deliveryAdress;
        CreatedBy = customerName;
        EditedBy = customerName;
        CreatedOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;

        Total = 0;
        foreach(var item in Products)
        {
            Total += item.Price;
        }

        Validate();             
    }

    private void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNullOrEmpty(CustomerId, "Customer")
            .IsTrue(Products != null && Products.Any(), "Products")
            .IsNotNullOrEmpty(DeliveryAddress, "Address");
            AddNotifications(contract);
    }
   

}