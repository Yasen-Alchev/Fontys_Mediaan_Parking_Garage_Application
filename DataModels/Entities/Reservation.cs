using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int SpotId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
    }
}
