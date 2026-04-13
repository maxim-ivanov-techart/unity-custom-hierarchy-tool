using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HierarchyEnhancer.Editor
{
    public class MissingComponentDrawer : IHierarchyDrawer
    {
        private static GUIStyle _warningStyle;
        private static readonly Dictionary<int, bool> _cache = new Dictionary<int, bool>();
        
        private static GUIStyle WarningStyle
        {
            get
            {
                if (_warningStyle == null)
                {
                    _warningStyle = new GUIStyle(EditorStyles.label)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        fontStyle = FontStyle.Bold,
                        fontSize = 14,
                        normal =
                        {
                            textColor = Color.yellow
                        }
                    };
                }
                return _warningStyle;
            }
        }
        public int Order => 1;
        
        public MissingComponentDrawer()
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

            bool hasMissing = false;

            if (!_cache.TryGetValue(instanceId, out hasMissing))
            {
                if (GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0)
                {
                    hasMissing = true;
                }
                
                if (!hasMissing)
                {
                    hasMissing = HasMissingReference(gameObject);
                }
                _cache[instanceId] = hasMissing;
            }
            
            if (hasMissing)
            {
                float markerWidth = 20f;
                Rect markerRect = new Rect(
                    rect.x + rect.width - markerWidth,
                    rect.y,
                    markerWidth,
                    rect.height
                    );
                EditorGUI.LabelField(markerRect, "⚠", WarningStyle);
            }
        }

        private bool HasMissingReference(GameObject gameObject)
        {
            Component[] components = gameObject.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    continue;
                }
                
                SerializedObject serializedObject = new SerializedObject(component);
                SerializedProperty iterator =  serializedObject.GetIterator();

                while (iterator.NextVisible(true))
                {
                    if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        if (iterator.objectReferenceValue == null && iterator.objectReferenceInstanceIDValue != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
