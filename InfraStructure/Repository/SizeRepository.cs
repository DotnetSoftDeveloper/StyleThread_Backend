using Application.Interfaces.IRepository;
using Domain.Entities;
using InfraStructure.Repository;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository
{
    internal class SizeRepository(ApplicationDbContext _dbContext) : Repository<Size>(_dbContext), ISizeRepository
    {
    }
}
