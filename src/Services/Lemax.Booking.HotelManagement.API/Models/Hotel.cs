using System;
using NetTopologySuite.Geometries;

namespace BookingManagement.API.Models
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Point Location { get; set; }

        public static Hotel New(string name, decimal price, Point location)
        {
            return new Hotel
            {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price,
                Location = location
            };
        }

        public Hotel WithName(string name)
        {
            Name = name;
            return this;
        }

        public Hotel WithPrice(decimal price)
        {
            Price = price;
            return this;
        }

        public Hotel WithLocation(Point location)
        {
            Location = location;
            return this;
        }
    }
}
