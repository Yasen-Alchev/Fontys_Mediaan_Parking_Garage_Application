using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Entities;

public class Stay
{
    public int Id { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? LeaveTime { get; set; }
    public int CarId { get; set; }
}
