using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HierarchyEnhancer.Editor
{
    [InitializeOnLoad]
    public static class HierarchyDispatcher
    {
        private static List<IHierarchyDrawer> _hierarchyDrawers;
        
        static HierarchyDispatcher()
        {
            _hierarchyDrawers = new List<IHierarchyDrawer>();
            _hierarchyDrawers.Add(new SeparatorDrawer());
            _hierarchyDrawers.Sort((a, b) => a.Order.CompareTo(b.Order));
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        static void OnHierarchyGUI(int instanceID, Rect rect)
        {
            foreach (var hierarchyDrawer in _hierarchyDrawers)
            {
                hierarchyDrawer.Draw(instanceID, rect);
            }
        }
    }
}
