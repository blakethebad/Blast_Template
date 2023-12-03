using Blast.EditorTools.LevelEditor.Code.Tools;
using Blast.Services.AssetManagement;
using UnityEditor;
using UnityEngine;

namespace Blast.EditorTools.LevelEditor.Code
{
    public class LevelEditorToolBoxDrawer
    {

        private LevelEditorWindow _levelEditorWindow;
        private float _toolBoxWidth = 500;
        private Vector2 _scrollPos = Vector2.zero;
        
        private const string SCROLL_POS_KEY = "LE_ToolBox_ScrollPos";

        private GUIStyle _toolBoxStyle = null;

        private Vector2Int _toolButtonSize = new Vector2Int(60, 60);

        #region ToolTypes

        private MainElementTypes _selectedMainElement;
        public enum MainElementTypes{ ColorStones, Boosters, Collectibles, Blockers, Spawners };


        private int _selectedToolIndex = -1; 

        #endregion

        public LevelEditorToolBoxDrawer(LevelEditorWindow levelEditorWindow)
        {
            _levelEditorWindow = levelEditorWindow;
        }

        public void ResetSelectedTool()
        {
            _selectedToolIndex = -1;

            _levelEditorWindow.SetSelectedTool(null);
        }


            public void Draw()
        {
            SetStyle();
            
            EditorGUILayout.BeginVertical(_toolBoxStyle, GUILayout.ExpandHeight(true));
            {
                Header();
                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                {
                    
                    EditorPrefs.SetFloat(SCROLL_POS_KEY, _scrollPos.y);
                    Body();

                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }

        private void SetStyle()
        {
            _toolBoxStyle = new GUIStyle(EditorStyles.helpBox);
            _toolBoxStyle.alignment = TextAnchor.UpperRight;
            _toolBoxStyle.fixedWidth = _toolBoxWidth;
        }
        
        private void Header()
        {
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("Tool Box");
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
        }

        private void Body()
        {
            EditorGUI.BeginChangeCheck();
            _selectedMainElement = (MainElementTypes) EditorGUILayout.EnumPopup("Main Type", _selectedMainElement);

            if (EditorGUI.EndChangeCheck())
            {
                ResetSelectedTool();
            }

            GUILayout.Space(20);
            
            SubElements();
        }

        private void SubElements()
        {
            switch (_selectedMainElement)
            {
                case MainElementTypes.ColorStones:
                    ColorStoneTools();
                    break;
                case MainElementTypes.Boosters:
                    BoosterTools();
                    break;
                case MainElementTypes.Blockers:
                    BlockerTools();
                    break;
                case MainElementTypes.Collectibles:
                    CollectibleTools();
                    break;
                case MainElementTypes.Spawners:
                    SpawnerTools();
                    break;
            }
        }

        private void ColorStoneTools()
        {
            ToolButtonDrawer<ColorStoneEditorTool>("/Stones");
        }

        private void BoosterTools()
        {
            
        }

        private void BlockerTools()
        {
            
        }

        private void CollectibleTools()
        {
            
        }

        private void SpawnerTools()
        {
            
        }

        private void ToolButtonDrawer<T>(string slug) where T : TileElementEditorTool
        {
            GUIStyle _toolStyle = new GUIStyle();
            _toolStyle.fixedWidth = 0;
            EditorGUILayout.BeginHorizontal(_toolStyle);
            {
                T[] tileElementTools = AssetManager.LevelEditor.GetLevelEditorTileElementTools<T>(slug);
                
                if (tileElementTools.Length > 0)
                {
                    GUIContent[] guiContent = new GUIContent[tileElementTools.Length + 1];

                    for (int i = 0; i < tileElementTools.Length; i++)
                    {
                        if (tileElementTools[i].Sprite)
                            guiContent[i + 1] = new GUIContent(tileElementTools[i].Sprite.texture, tileElementTools[i].ToolTip);
                        else
                            guiContent[i + 1] = new GUIContent(tileElementTools[i].Name, tileElementTools[i].ToolTip);
                    }

                    guiContent[0] = new GUIContent("Remove");
                    
                    EditorGUI.BeginChangeCheck();
                    _selectedToolIndex = GUILayout.Toolbar(_selectedToolIndex, guiContent,
                        GUILayout.Width(_toolButtonSize.x * guiContent.Length), GUILayout.Height(_toolButtonSize.y));

                    if (EditorGUI.EndChangeCheck())
                    {
                        if (_selectedToolIndex > 0)
                        {
                            SetSelectedTool(tileElementTools[_selectedToolIndex - 1]);
                            //Selected tool is set
                        }
                        else
                        {
                            SelectEraseTool(tileElementTools[0].ElementType);
                            //Erasing tool is set
                        }
                    }
                }
                
                _levelEditorWindow.ToolBoxRect = GUILayoutUtility.GetLastRect();//this is for the DeSelection operation

            }
            EditorGUILayout.EndHorizontal();
        }

        public void SetSelectedTool(TileElementEditorTool selectedTool)
        {
            bool canSelect = _levelEditorWindow.SetSelectedTool(selectedTool);

            if (!canSelect)
            {
                ResetSelectedTool();
            }
        }

        public void SelectEraseTool(BoardElementType type)
        {
            
        }
        
    }
}