using Repository.Entities;
using Repository.Entities.DisplayBlockEntities;
using System;
using System.Linq.Expressions;

namespace Repository.Specifications
{
    public static class DisplayBlockSpecification
    {
        public static Expression<Func<T, bool>> BySceneId<T>(int? sceneId) where T : DisplayBlockEntity
        {
            return b => b.SceneId == sceneId;
        }

        internal static Expression<Func<T, bool>> ById<T>(int id) where T : DisplayBlockEntity
        {
            return b => b.Id == id;
        }
    }
}
