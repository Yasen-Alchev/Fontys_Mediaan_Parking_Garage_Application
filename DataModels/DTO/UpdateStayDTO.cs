using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.DTO;

public class UpdateStayDTO
{
    public UpdateStayDTO(DateTime? leaveTime, bool hasPaid)
    {
        LeaveTime = leaveTime;
        this.hasPaid = hasPaid;
    }

    public DateTime? LeaveTime { get; set; }
    public bool hasPaid { get; set; }
}
