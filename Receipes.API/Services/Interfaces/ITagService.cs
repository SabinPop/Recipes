using Recipes.API.Models.Entities;
using System.Linq;

namespace Recipes.API.Services.Interfaces
{
    public interface ITagService
    {
        public bool Exists(TagEntity tag);
        public bool Exists(int id);

        public bool CreateTag(TagEntity tag);
        public TagEntity GetTagById(int id);
        public bool UpdateTag(TagEntity tag);
        public bool DeleteTag(int id);

        public IQueryable<TagEntity> GetAllTags();

    }
}
