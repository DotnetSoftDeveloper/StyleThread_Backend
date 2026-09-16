using Application.DTO;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.Extensions.Options;
using InfraStructure.Repository;
using InfraStructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    internal class CustomerRepository(ApplicationDbContext _dbContext) : Repository<Customer>(_dbContext), ICustomerRepository
    {
    }
}
