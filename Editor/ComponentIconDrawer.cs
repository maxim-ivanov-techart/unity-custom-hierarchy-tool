using System.Collections.Generic;
using HierarchyEnhancer.Editor;
using UnityEditor;
using UnityEngine;
namespace HierarchyEnhancer.Editor
{
    public class ComponentIconDrawer : IHierarchyDrawer
    {
        private const float ICON_SIZE = 16f;
        private const float RIGHT_PADDING = 24f;
        private readonly Dictionary<int, List<Texture>> _cache = new();
        public int Order => 2;

        public ComponentIconDrawer()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private void OnHierarchyChanged()
        {
            _cache.Clear();
        }
        public void Draw(int instanceId, Rect rect)
        {
            GameObject gameObject = EditorUtility.EntityIdToObject(instanceId) as GameObject;
            if (gameObject == null)
            {
                return;
            }
            Component[]  components = gameObject.GetComponents<Component>();
            List<Texture> icons;

            if (!_cache.TryGetValue(instanceId, out icons))
            {
                icons = CollectIcons(gameObject);
                _cache[instanceId] = icons;
            }
            
            if (icons.Count == 0)
            {
                return;
            }
                
            float startX = rect.x + rect.width - RIGHT_PADDING - (icons.Count * ICON_SIZE);
            for (int i = 0; i < icons.Count; i++)
            {
                Rect iconRect = new Rect(
                    startX + (i * ICON_SIZE),
                    rect.y,
                    ICON_SIZE,
                    ICON_SIZE
                );
                GUI.DrawTexture(iconRect, icons[i], ScaleMode.ScaleToFit);
            }
        }

        private List<Texture> CollectIcons(GameObject gameObject)
        {
            List<Texture> icons = new List<Texture>();
            Component[] components = gameObject.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    continue;
                }

                if (component is Transform)
                {
                    continue;
                }

                GUIContent content = EditorGUIUtility.ObjectContent(component, component.GetType());
                if (content.image != null && !content.image.name.Contains("DefaultAsset"))
                {
                    icons.Add(content.image);
                }
            }
            return icons;
        }
    }
}
