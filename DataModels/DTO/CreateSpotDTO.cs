using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.DTO;

public class CreateSpotDTO
{
    public int Id { get; set; }
    public int Status { get; set; }
    public int ParkingId { get; set; }
}
