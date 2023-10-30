using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Entities
{
    public class Parking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Spot> spots { get; set; } = new List<Spot>();
    }
}
