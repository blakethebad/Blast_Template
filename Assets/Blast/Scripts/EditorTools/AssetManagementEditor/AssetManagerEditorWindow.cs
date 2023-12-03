using Blast.Scripts.Services.AssetManagement.AssetGroup;
using UnityEditor;
using UnityEngine;

namespace Blast.Scripts.EditorTools.AssetManagementEditor
{
    public class AssetManagerEditorWindow : EditorWindow
    {
        Vector2 _scrollPos = Vector2.zero;
        Vector2 _inspectorScrollPos = Vector2.zero;
        private BoardElementAssetGroup _boardElementAssetGroup;
        private LevelAssetGroup _levelAssetGroup;
        

        [MenuItem("Blast/AssetManager")]
        static void ShowWindow()
        {
            // Show the editor window
            AssetManagerEditorWindow window = GetWindow<AssetManagerEditorWindow>("Asset Manager");
        }

        private void OnGUI()
        {
            GUIStyle assetManagementSelection = new GUIStyle(EditorStyles.helpBox);
            assetManagementSelection.alignment = TextAnchor.UpperRight;
            assetManagementSelection.fixedWidth = 200f;
            
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical(assetManagementSelection, GUILayout.MaxWidth(200), GUILayout.ExpandHeight(true));
                {
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                    {
                        if (GUILayout.Button("Board Element Prefabs"))
                        {
                            string[] assetGuiIDs = AssetDatabase.FindAssets($"t:{typeof(BoardElementAssetGroup).FullName}", 
                                new[] { "Assets/Blast/GameAssets/AssetGroups" });

                            foreach (var guiID in assetGuiIDs)
                            {
                                _boardElementAssetGroup = AssetDatabase.LoadAssetAtPath<BoardElementAssetGroup>(AssetDatabase.GUIDToAssetPath(guiID));
                            }

                            if (_boardElementAssetGroup != null)
                            {
                                _levelAssetGroup = null;
                            }
                        }

                        if (GUILayout.Button("Level Assets"))
                        {
                            _boardElementAssetGroup = null;
                        }

                        if (GUILayout.Button("VFX Assets"))
                        {
                            _boardElementAssetGroup = null;
                            _levelAssetGroup = null;
                        }

                        if (GUILayout.Button("UI Assets"))
                        {
                            _boardElementAssetGroup = null;
                            _levelAssetGroup = null;
                        }
                    }
                    EditorGUILayout.EndScrollView();
                } EditorGUILayout.EndVertical();
                
                if (_boardElementAssetGroup)
                {
                    DrawBoardElementPrefabs();
                }

                if (_levelAssetGroup)
                {
                    DrawLevelPrefabs();
                }
                
            }
            EditorGUILayout.EndHorizontal();
            
            
            
        }

        private void DrawBoardElementPrefabs()
        {
            _inspectorScrollPos = EditorGUILayout.BeginScrollView(_inspectorScrollPos);
            {
                BoardElementAssetGroup targetAsset = _boardElementAssetGroup;
                if (GUILayout.Button("Update Table"))
                {
                    targetAsset.UpdateDictionary();
                }

                if (targetAsset.BoardElementPrefabs != null)
                {
                    EditorGUILayout.BeginVertical();
                    foreach (var pair in targetAsset.BoardElementPrefabs)
                    {
                        if(pair.Key == BoardElementType.None || pair.Key == BoardElementType.RandomStone)
                            continue;
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label(pair.Key.ToString());
                        EditorGUI.BeginChangeCheck();
                        pair.Value = (GameObject)EditorGUILayout.ObjectField(pair.Value, typeof(GameObject), false, GUILayout.Height(20), GUILayout.Width(200));
                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(targetAsset);
                            AssetDatabase.SaveAssetIfDirty(targetAsset);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void DrawLevelPrefabs()
        {
            
        }
    }
}