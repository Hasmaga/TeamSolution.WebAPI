﻿using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class StoreServiceRepository : IStoreServiceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public StoreServiceRepository(ApplicationDbContext context, ILogger<StoreServiceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<StoreService> GetStoreServiceByIdRepositoryAsync(Guid id)
        {
            try
            {
                var storeService = await _context.StoreServices.FirstOrDefaultAsync(x => x.Id == id);
                if (storeService != null)
                {
                    return storeService;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
