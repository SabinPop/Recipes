using Microsoft.EntityFrameworkCore;
using Recipes.Server.Data;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using Recipes.Shared.Paging;
using System;
using System.Linq;

namespace Recipes.Server.Services
{
    public class TagService : IRepository<TagEntity, int>
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(TagEntity tag)
        {
            if (tag is null)
                return false;
            if (Exists(tag))
                return false;
            try
            {
                _context.Tags.Add(tag);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            if (Exists(id) == false)
                return false;
            try
            {
                var tag = GetById(id);
                _context.Tags.Remove(tag);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Exists(TagEntity tag)
        {
            return _context.Tags.AsNoTracking().Any(t => t.Name == tag.Name);
        }

        public bool Exists(int id)
        {
            return _context.Tags.AsNoTracking().Any(t => t.TagId == id);
        }

        public IQueryable<TagEntity> GetAll()
        {
            return _context.Tags.AsNoTracking().Include(t => t.Recipes).Select(x => x).Where(x => x.Recipes.Count > 0);
        }

        public TagEntity GetById(int id)
        {
            return _context.Tags.AsNoTracking().FirstOrDefault(t => t.TagId == id);
        }

        public PagedList<TagEntity> GetPage(Parameters parameters)
        {
            var tags = GetAll().ToList();
            return PagedList<TagEntity>
                .ToPagedList(tags,
                    parameters.PageNumber,
                    parameters.PageSize);
        }

        public IQueryable<TagEntity> GetStartingWithString(string name)
        {
            return _context.Tags.AsNoTracking().Where(i => i.Name.ToLower().StartsWith(name.ToLower()));
        }

        public bool Update(TagEntity tag)
        {
            if (tag is null)
                return false;
            if (Exists(tag) == false)
                return false;
            try
            {
                _context.Tags.Update(tag);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
