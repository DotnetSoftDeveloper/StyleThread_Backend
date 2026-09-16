using Application.Interfaces.IRepository;
using Domain.Entities;
using InfraStructure.Repository;
using InfraStructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    internal class NeckTypeRepository(ApplicationDbContext _dbContext) : Repository<NeckType>(_dbContext), INeckTypeRepository
    { 
    }
}
