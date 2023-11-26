using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.DTO;

public class CreateStayDTO
{
    public CreateStayDTO(DateTime entryTime, DateTime? leaveTime, int carId)
    {
        EntryTime = entryTime;
        LeaveTime = leaveTime;
        CarId = carId;
    }
    public DateTime EntryTime { get; set; }
    public DateTime? LeaveTime { get; set; }
    public int CarId { get; set; }
}
