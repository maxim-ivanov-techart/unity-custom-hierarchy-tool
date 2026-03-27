using UnityEditor;
using UnityEngine;
using System.IO;

namespace HierarchyEnhancer.Editor
{
    [CreateAssetMenu(menuName = "Custom Hierarchy Tools/Hierarchy Settings")]
    public class HierarchySettings : ScriptableObject
    {
        private const string DEFAULT_FOLDER_PATH = "Assets/Editor/CustomHierarchyTools/Settings";
        private const string DEFAULT_ASSET_PATH = DEFAULT_FOLDER_PATH + "/HierarchySettings.asset";

        private static HierarchySettings _instance;

        public static HierarchySettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    var guids = AssetDatabase.FindAssets("t:HierarchySettings");
                    if (guids.Length > 0)
                    {
                        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                        _instance = AssetDatabase.LoadAssetAtPath<HierarchySettings>(path);
                    }
                    else
                    {
                        _instance = CreateInstance<HierarchySettings>();
                        if (!Directory.Exists(DEFAULT_FOLDER_PATH))
                        {
                            Directory.CreateDirectory(DEFAULT_FOLDER_PATH);
                            AssetDatabase.Refresh();
                        }
                        AssetDatabase.CreateAsset(_instance, DEFAULT_ASSET_PATH);
                        AssetDatabase.SaveAssets();
                    }
                }

                return _instance;
            }
        }

        [Header("Separator")] 
        public Color separatorBackgroundColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        public Color separatorTextColor = Color.white;
        public string separatorText = "---";
    }
}
