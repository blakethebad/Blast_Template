using Blast.Scripts.Core.Grid.GridData;
using UnityEditor;
using UnityEngine;

namespace Blast.Scripts.EditorTools.LevelEditor.Code
{
    public class LevelEditorViewDrawer
    {
        private LevelEditorWindow _levelEditorWindow;
        private LevelEditorGridDrawer _gridDrawer;
        private LevelEditorGridSettingsDrawer _gridSettingsDrawer;
        private LevelEditorPlacementHandler _placementHandler;
        
        private Vector2 _scrollPos = Vector2.zero;

        private LevelData _level;
        private LevelData _originalLevel;

        private int _levelSizeX;
        private int _levelSizeY;
        

        private GUIStyle _style = new GUIStyle();

        public LevelEditorViewDrawer(LevelEditorWindow levelEditorWindow)
        {
            _levelEditorWindow = levelEditorWindow;
            _gridDrawer = new LevelEditorGridDrawer(OnTileRightClicked, OnTileLeftClicked);
            _gridSettingsDrawer = new LevelEditorGridSettingsDrawer();
            _placementHandler = new LevelEditorPlacementHandler(levelEditorWindow);
        }
        
        public void DrawLevelEditorView()
        {
            _style.fixedWidth = 0;
            _style.fixedHeight = 0;

            if (!_levelEditorWindow.SelectedLevelData)
            {
                _level = null;
                _originalLevel = null;
                _levelEditorWindow.SelectedLevelIndex = -1;
                return;
            }
            
            SetLevelReferences();

            EditorGUILayout.BeginVertical(_style,GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            {
                DrawViewPanel();
                
                DrawBottomSelectionButtons();

            } EditorGUILayout.EndVertical();
        }

        private void SetLevelReferences()
        {
            if (!_originalLevel || _originalLevel != _levelEditorWindow.SelectedLevelData)
            {
                _originalLevel = _levelEditorWindow.SelectedLevelData;
                _level = UnityEngine.Object.Instantiate(_levelEditorWindow.SelectedLevelData);
                
                _levelSizeX = _level.sizeX;
                _levelSizeY = _level.sizeY;
                
                //Save visual layer
            }
        }

        private void DrawViewPanel()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            {
                _gridDrawer.DrawGrid(_level);
                _gridSettingsDrawer.DrawGridSettings(_level, ref _levelSizeX, ref _levelSizeY);
                        
                GUILayout.FlexibleSpace();

                GUILayout.Space(50);

            } EditorGUILayout.EndScrollView();
        }

        private void DrawBottomSelectionButtons()
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("SAVE", GUILayout.ExpandWidth(true), GUILayout.Height(50)))
                    _levelEditorWindow.SaveLevel(_level);
                    

                if (GUILayout.Button("CLEAR", GUILayout.ExpandWidth(true), GUILayout.Height(50)))
                    _levelEditorWindow.ClearSelectedLevel();
            }
            EditorGUILayout.EndHorizontal();
        }


        private void OnTileLeftClicked(int tileDataIndex) => _placementHandler.OnTileLeftClicked(_level, tileDataIndex);

        private void OnTileRightClicked(int tileDataIndex) => _placementHandler.OnTileRightClicked(_level, tileDataIndex);
    }
}