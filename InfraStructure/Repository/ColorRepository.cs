using Application.Interfaces.IRepository;
using Domain.Entities;
using InfraStructure.Repository;
using InfraStructure.Context;

namespace InfraStructure.Repository
{
    internal class ColorRepository(ApplicationDbContext _dbContext) : Repository<Color>(_dbContext), IColorRepository
    {
    }
}
