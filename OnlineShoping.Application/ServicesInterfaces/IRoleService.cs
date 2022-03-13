using OnlineShoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesInterfaces
{
    public interface IRoleService
    {
        Task<List<Role>> Get();
     }
}
