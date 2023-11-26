using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.DTO;
public class CarLeaveDTO
{
    public CarLeaveDTO(int carId)
    {
        CarId = carId;
    }

    public int CarId { get; set; }
}
