using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Application.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public IdNameResource Model { get; set; }
        public IdNameResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<IdNameResource> Features { get; set; }
        public VehicleResource()
        {
            Features = new Collection<IdNameResource>();
        }
    }
}