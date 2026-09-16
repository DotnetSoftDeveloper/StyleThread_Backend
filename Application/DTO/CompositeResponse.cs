using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CompositeResponse
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Fit> Fits { get; set; }
        public List<Fabric> Fabrics { get; set; }
        public List<Sleeve> Sleeves { get; set; }
        public List<NeckType> NeckTypes { get; set; }
        public List<FabricCare> FabricCares { get; set; }
        public List<Color> Color { get; set; }
        public List<Size> Sizes { get; set; }
    }
}
