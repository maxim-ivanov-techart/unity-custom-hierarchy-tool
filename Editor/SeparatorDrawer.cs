using UnityEditor;
using UnityEngine;

namespace HierarchyEnhancer.Editor
{
    public class SeparatorDrawer
    {
        private static GUIStyle _separatorStyle;
        
        private static GUIStyle SeparatorStyle
        {
            get
            {
                if (_separatorStyle == null)
                {
                    _separatorStyle = new GUIStyle(EditorStyles.label)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        fontStyle = FontStyle.Bold,
                        normal =
                        {
                            textColor = Color.white
                        }
                    };
                }
                return _separatorStyle;
            }
        }
        
        public static void Draw(int instanceId, Rect rect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (gameObject != null)
            {
                if (gameObject.name.StartsWith("---"))
                {
                    if (!gameObject.CompareTag("EditorOnly"))
                    {
                        gameObject.tag = "EditorOnly";
                    }
                    string newName = gameObject.name.Replace("---", "").Trim();
                    EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
                    EditorGUI.LabelField(rect, newName, SeparatorStyle);
                }
            }
        }
    }
}
