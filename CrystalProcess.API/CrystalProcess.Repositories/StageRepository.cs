using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrystalProcess.Data;
using CrystalProcess.Models;
using Microsoft.EntityFrameworkCore;

namespace CrystalProcess.Repositories
{
    public class StageRepository:IStageRepository
    {
        private readonly ApplicationDbContext _context;

        public StageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IList<Stage>> Get()
        {
           return await _context.Stages.ToListAsync();
        }

        public async Task Add(Stage entity)
        {
            _context.Stages.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
