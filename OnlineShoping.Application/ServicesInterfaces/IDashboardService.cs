using OnlineShoping.Application.DTOs.OutputDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesInterfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetStatistics();

    }
}
