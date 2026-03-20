using UnityEngine;

namespace HierarchyEnhancer.Editor
{
    public interface IHierarchyDrawer
    {
        int Order
        {
            get;
        }
        
        void Draw(int instanceId, Rect rect);
    } 
}