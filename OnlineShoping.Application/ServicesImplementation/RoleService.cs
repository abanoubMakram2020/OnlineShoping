using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<List<Role>> Get()
        {
            return await _roleRepository.Get().ToListAsync();
        }
    }
}
