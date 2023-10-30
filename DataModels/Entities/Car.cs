using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public int UserId { get; set; }
        public Spot Spot { get; set; }
    }
}
