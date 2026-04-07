using System;
using System.Collections.Generic;
using System.Reflection;
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
            Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in allTypes)
            {
                if (type.IsInterface || type.IsAbstract)
                {
                    continue;
                }

                if (typeof(IHierarchyDrawer).IsAssignableFrom(type))
                {
                    IHierarchyDrawer hierarchyDrawer = (IHierarchyDrawer)Activator.CreateInstance(type);
                    _hierarchyDrawers.Add(hierarchyDrawer);
                }
            }
            
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
