﻿namespace Project.Business.Models;

public class Address : Entity
{
      public Guid ProviderId { get; set; }
      public string Street { get; set; }
      public string Number { get; set; }
      public string Complement { get; set; }
      public string ZipCode { get; set; }
      public string Neighborhood { get; set; }
      public string City { get; set; }
      public string State { get; set; }

      /* EF Relation */
      public Provider Provider { get; set; }
}
