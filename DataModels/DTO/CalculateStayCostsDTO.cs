using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.DTO;

public class CalculateStayCostsDTO
{
    public string LicensePlate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
