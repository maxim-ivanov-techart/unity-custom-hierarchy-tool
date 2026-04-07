using UnityEditor;
using UnityEngine;

namespace HierarchyEnhancer.Editor
{
    public class MissingComponentDrawer : IHierarchyDrawer
    {
        private static GUIStyle _warningStyle;

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

        public void Draw(int instanceId, Rect rect)
        {
            GameObject gameObject = EditorUtility.EntityIdToObject(instanceId) as GameObject;
            if (gameObject == null)
            {
                return;
            }

            int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject);
            if (count > 0)
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
    }
}
