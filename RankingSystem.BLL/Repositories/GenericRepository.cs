using Microsoft.EntityFrameworkCore;
using RankingSystem.BLL.Interfaces;
using RankingSystem.DAL.DbContexts;
using RankingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingSystem.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SimpleRatingSystemDbContext _dbContext;

        public GenericRepository(SimpleRatingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public  int AddAsync(T item)
        {
             _dbContext.AddAsync(item);
            return _dbContext.SaveChanges();

        }

        public int DeleteAsync(T item)
        {
             _dbContext.Remove(item);
            return _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // implement Specifications Design Pattern is a benfit from This Way
            if(typeof(T) == typeof(Item))
            {
                return (IEnumerable<T>)await _dbContext.Items.Include(I => I.Ratings).ToListAsync();
            }
            else if (typeof(T) == typeof(Rating))
            {
                return (IEnumerable<T>)await _dbContext.Ratings.OrderBy(R=>R.ItemId).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public int  UpdateAsync(T item)
        {
             _dbContext.Update(item);
            return _dbContext.SaveChanges();
        }
    }
}
