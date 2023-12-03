using Blast.Services.AssetManagement;
using UnityEditor;
using UnityEngine;

namespace Blast.EditorTools.LevelEditor.Code
{
    public class LevelEditorExplorerDrawer
    {
        private LevelEditorWindow _levelEditorWindow;
        
        public Vector2 _scrollPos = Vector2.zero;
        private const string SCROLL_POS_KEY = "LE_LevelExplorer_scrollPos";

        public LevelEditorExplorerDrawer(LevelEditorWindow levelEditorWindow)
        {
            _levelEditorWindow = levelEditorWindow;
        }
        
        public void DrawLevelSelectionView()
        {
            GUIStyle levelSelectionStyle = new GUIStyle(EditorStyles.helpBox);
            levelSelectionStyle.alignment = TextAnchor.UpperRight;
            levelSelectionStyle.fixedWidth = 150f;
            
            EditorGUILayout.BeginVertical(levelSelectionStyle, GUILayout.MaxWidth(150f), GUILayout.ExpandHeight(true));
            {
                GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("Levels");
                GUI.skin.label.alignment = TextAnchor.UpperLeft;
                
                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                {
                    EditorPrefs.SetFloat(SCROLL_POS_KEY, _scrollPos.y);
            
                    EditorGUI.BeginChangeCheck();
                    _levelEditorWindow.SelectedLevelIndex = GUILayout.SelectionGrid(_levelEditorWindow.SelectedLevelIndex, AssetManager.LevelEditor.LevelDisplayNames, 1);

                    if (EditorGUI.EndChangeCheck())
                    {
                        _levelEditorWindow.SetSelectedLevelData();
                    }

                } EditorGUILayout.EndScrollView();
                
                // Draw the "New Level" button
                if (GUILayout.Button("New"))
                {
                    AssetManager.LevelEditor.CreateNewLevelAsset();
                }
                
            } EditorGUILayout.EndVertical();
        }
    }
}