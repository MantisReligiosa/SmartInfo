using Repository.Entities;
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
    }
}
