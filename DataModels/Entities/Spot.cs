using DataModels.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Entities;

public class Spot
{
    public int Id { get; set; }
    public SpotStatus Status { get; set; }
    public int ParkingId { get; set; }
    public int? CarId { get; set; }
}
