using Blast.Core.Grid.GridData;
using Blast.EditorTools.LevelEditor.Code.Tools;
using Blast.Services.AssetManagement;
using UnityEditor;
using UnityEngine;

namespace Blast.EditorTools.LevelEditor.Code
{
    public class LevelEditorWindow : EditorWindow
    {
        private LevelEditorViewDrawer ViewDrawer;
        private LevelEditorExplorerDrawer ExplorerDrawer;
        private LevelEditorGridSettingsDrawer GridSettingsDrawer;
        private LevelEditorToolBoxDrawer ToolBoxDrawer;
        
        public Rect ToolBoxRect { get; set; }
        
        public TileElementEditorTool SelectedTool { get; private set; }
        
        public TileData SelectedTile { get; private set; }
        
        // The level data asset
        public LevelData SelectedLevelData { get; set; }
        //Index of the currently selected level data
        public int SelectedLevelIndex { get; set; } = -1;

        public LevelEditorWindow()
        {
            ExplorerDrawer = new LevelEditorExplorerDrawer(this);
            ViewDrawer = new LevelEditorViewDrawer(this);
            ToolBoxDrawer = new LevelEditorToolBoxDrawer(this);
        }
        
        [MenuItem("Blast/LevelEditor")]
        static void ShowWindow()
        {
            // Show the editor window
            LevelEditorWindow window = GetWindow<LevelEditorWindow>("Level Editor");
        }

        private void DrawLabel()
        {
            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUILayout.Label("Blast Level Editor", EditorStyles.boldLabel);
        }
        
        void OnGUI()
        {
            //Generate a label on top of the level editor that can act like a header
            DrawLabel();
            
            EditorGUILayout.BeginHorizontal();
            {
                //Generate a list of buttons on the left side on the window to select levels
                 ExplorerDrawer.DrawLevelSelectionView();

                 EditorGUILayout.BeginVertical();
                 {
                     //Draw toolbox
                     //Draw middle window there the editor can edit the level
                     ViewDrawer.DrawLevelEditorView();

                 } EditorGUILayout.EndVertical();
                 
                 //Draw inspector to select properties of selected tool
                 ToolBoxDrawer.Draw();
                 
            } EditorGUILayout.EndHorizontal();


            if (Event.current.type == EventType.MouseDown)
            {
                if (IsClickedEmptyField(Event.current.mousePosition))
                {
                    ToolBoxDrawer.ResetSelectedTool();
                    SetSelectedTile(null);
                }
            }
            
            MouseInfoRect();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void MouseInfoRect()
        {
            Event e = Event.current;
            Vector2 mousePosition = e.mousePosition;
            Rect cursorRect = new Rect(mousePosition.x + 10, mousePosition.y + 10, 40, 40);

            if (SelectedTool != null)
            {
                Texture2D icon = GetIcon(SelectedTool);
                if (icon)
                    EditorGUI.DrawTextureTransparent(cursorRect, icon);
                else
                {
                    EditorGUI.DrawRect(cursorRect, Color.gray);
                }
            }

        }

        public Texture2D GetIcon(TileElementEditorTool tool)
        {
            string slug = "";

            switch (tool.ElementType)
            {
                case (BoardElementType.BlueStone):
                    slug = $"/{BoardElementType.BlueStone.ToString()}";
                    break;
                
                case (BoardElementType.YellowStone):
                    slug = $"/{BoardElementType.YellowStone.ToString()}";
                    break;
                
                case (BoardElementType.RedStone):
                    slug = $"/{BoardElementType.RedStone.ToString()}";
                    break;
                
                case (BoardElementType.PurpleStone):
                    slug = $"/{BoardElementType.PurpleStone.ToString()}";
                    break;
                
                case (BoardElementType.GreenStone):
                    slug = $"/{BoardElementType.GreenStone.ToString()}";
                    break;
                default:
                    Debug.LogError("INVALID TYPE");
                    break;
            }
            

            return AssetManager.LevelEditor.GetIcon(slug);
        }
        
        private bool IsClickedEmptyField(Vector2 clickPosition)
        {
            if (ToolBoxRect.Contains(clickPosition))
                return false;
            else
                return true;
        }

        public bool SetSelectedTool(TileElementEditorTool tool)
        {
            if (tool != null && SelectedLevelData)
            {
                SelectedTool = tool;
                SetSelectedTile(null);
                return true;
            }
            else
            {
                SelectedTool = null;
                return false;
            }
        }

        public bool SetSelectedTile(TileData tileData)
        {
            if (tileData != null && SelectedLevelData)
            {
                SelectedTile = tileData;
                return true;
            }
            else
            {
                SelectedTile = null;
                return false;
            }
        }

        public void SetSelectedLevelData()
        {
            if (SelectedLevelIndex >= 0 && SelectedLevelIndex < AssetManager.LevelEditor.LevelInfos.Count)
            {
                LevelInfo levelInfo = AssetManager.LevelEditor.LevelInfos[SelectedLevelIndex];
                string levelGuiId = levelInfo.GuiId;
                LevelData levelsDataAsset = AssetDatabase.LoadAssetAtPath<LevelData>(AssetDatabase.GUIDToAssetPath(levelGuiId));
                SelectedLevelData = levelsDataAsset;
            }
        }

        public void SaveLevel(LevelData levelData)
        {
            AssetManager.LevelEditor.SaveLevel(levelData, SelectedLevelData);
            
            if (AssetManager.LevelEditor.TryGetLevelInfoByLevelNumber(levelData.levelIndex, out LevelInfo levelInfoForIndex))
                SelectedLevelIndex = AssetManager.LevelEditor.LevelInfos.IndexOf(levelInfoForIndex);
            
            SetSelectedLevelData();
        }
        
        public void ClearSelectedLevel()
        {
            
        }

        public void OnEnable()
        {
            //Get levels on the project and put them in an array
            AssetManager.LevelEditor.RefreshLevelInfoList();
        }
    }
}