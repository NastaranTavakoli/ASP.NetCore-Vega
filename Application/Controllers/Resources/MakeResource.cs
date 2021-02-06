using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Application.Controllers.Resources
{
    public class MakeResource : IdNameResource
    {
        public ICollection<IdNameResource> Models { get; set; }

        public MakeResource()
        {
            Models = new Collection<IdNameResource>();
        }
    }
}