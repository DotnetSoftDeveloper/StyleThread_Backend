

using Application.Interfaces.IRepository;
using Domain.Entities;
using InfraStructure.Repository;
using InfraStructure.Context;

namespace InfraStructure.Repository
{
    internal class FabricCareRepository(ApplicationDbContext _dbContext) : Repository<FabricCare>(_dbContext), IFabricCareRepository
    {
    }
}
