using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.DTO;

public class UpdateSpotDTO
{
    public int Id { get; set; }
    public int Status { get; set; }
    public int? CarId { get; set; }
}
