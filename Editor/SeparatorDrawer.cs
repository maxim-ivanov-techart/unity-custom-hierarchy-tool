using UnityEditor;
using UnityEngine;

namespace HierarchyEnhancer.Editor
{
    public class SeparatorDrawer : IHierarchyDrawer
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
                            textColor = HierarchySettings.Instance.separatorTextColor
                        }
                    };
                }
                return _separatorStyle;
            }
        }
        
        public int Order => 0;

        public void Draw(int instanceId, Rect rect)
        {
            GameObject gameObject = EditorUtility.EntityIdToObject(instanceId) as GameObject;
            if (gameObject != null)
            {
                if (IsSeparator(gameObject, HierarchySettings.Instance.separatorText))
                {
                    if (!gameObject.CompareTag("EditorOnly"))
                    {
                        gameObject.tag = "EditorOnly";
                    }
                    string newName = gameObject.name.Replace(HierarchySettings.Instance.separatorText, "").Trim();
                    SeparatorStyle.normal.textColor =  HierarchySettings.Instance.separatorTextColor;
                    EditorGUI.DrawRect(rect, HierarchySettings.Instance.separatorBackgroundColor);
                    EditorGUI.LabelField(rect, newName, SeparatorStyle);
                }
            }
        }

        private bool IsSeparator(GameObject gameObject, string separatorText)
        {
            string objectName =  gameObject.name;
            if (objectName.StartsWith(separatorText))
            {
                if (objectName.Length == separatorText.Length)
                {
                    return true;
                }

                if(objectName[separatorText.Length] == ' ')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
