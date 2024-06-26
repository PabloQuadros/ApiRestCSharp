﻿namespace Project.Business.Models;

public class Product: Entity
{
    public Guid ProviderId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Enable { get; set; }

    /* EF Relations */
    public Provider Provider { get; set; }
}