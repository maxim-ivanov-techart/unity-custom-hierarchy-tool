using UnityEditor;
using UnityEngine;

namespace HierarchyEnhancer.Editor
{
    [InitializeOnLoad]
    public static class HierarchyDispatcher
    {
        static HierarchyDispatcher()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        static void OnHierarchyGUI(int instanceID, Rect rect)
        {
            SeparatorDrawer.Draw(instanceID, rect);
        }
    }
}
