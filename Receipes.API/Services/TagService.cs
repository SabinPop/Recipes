using Microsoft.EntityFrameworkCore;
using Recipes.API.Data;
using Recipes.API.Models.Entities;
using Recipes.API.Services.Interfaces;
using System;
using System.Linq;

namespace Recipes.API.Services
{
    public class TagService : ITagService
    {
        private readonly RecipesDbContext _context;

        public TagService(RecipesDbContext context)
        {
            _context = context;
        }

        public bool CreateTag(TagEntity tag)
        {
            if (tag == null)
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

        public bool DeleteTag(int id)
        {
            if (Exists(id) == false)
                return false;
            try
            {
                var tag = GetTagById(id);
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
            return _context.Tags.FirstOrDefault(t => t == tag) != null;
        }

        public bool Exists(int id)
        {
            return _context.Tags.FirstOrDefault(t => t.TagId == id) != null;
        }

        public IQueryable<TagEntity> GetAllTags()
        {
            return _context.Tags.AsNoTracking().Select(x => x);
        }

        public TagEntity GetTagById(int id)
        {
            return _context.Tags.FirstOrDefault(t => t.TagId == id);
        }

        public bool UpdateTag(TagEntity tag)
        {
            if (tag == null)
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
