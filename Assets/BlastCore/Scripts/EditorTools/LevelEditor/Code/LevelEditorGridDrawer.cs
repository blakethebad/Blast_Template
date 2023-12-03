using System;
using Match3.Grid.GridData;
using UnityEditor;
using UnityEngine;

namespace Match3.LevelEditor
{
    public class LevelEditorGridDrawer
    {
        public const int GRID_CELL_SIZE = 56;
        public const float GRID_CELL_PADDING = 5f;

        private Action<int> _onTileRightClicked;
        private Action<int> _onTileLeftClicked;

        private Vector2 _gridDrawPos;

        public LevelEditorGridDrawer(Action<int> onTileRightClicked, Action<int> onTileLeftClicked)
        {
            _onTileLeftClicked = onTileLeftClicked;
            _onTileRightClicked = onTileRightClicked;
        }
        
        private int GetIndex(LevelData levelData, in int x, in int y) => y * levelData.sizeX + x; //index calculation formula

        public void DrawGrid(LevelData levelData)
        {
            EditorGUILayout.BeginHorizontal(); 
            {
                GUILayout.FlexibleSpace();
                {
                    GUILayout.BeginVertical();
                    {
                        for (int y = levelData.sizeY - 1; y >= 0; y--)
                        {
                            GUILayout.BeginHorizontal();
                            {
                                for (int x = 0; x < levelData.sizeX; x++)
                                {
                                    DrawTile(levelData, levelData.TileDataList[GetIndex(levelData, x, y)]);
                                }
                            } 
                            GUILayout.EndHorizontal();
                        }
                    } 
                    GUILayout.EndVertical();   
                }
                GUILayout.FlexibleSpace();
            } 
            EditorGUILayout.EndHorizontal();
        }

        private void DrawTile(LevelData levelData, TileData tileData)
        {
            if (GUILayout.Button("", GUILayout.Width(GRID_CELL_SIZE), GUILayout.Height(GRID_CELL_SIZE)))
            {
                if (Event.current.button == 1)
                {
                    //Right Clicked
                    _onTileRightClicked.Invoke(GetIndex(levelData, tileData.xPos, tileData.yPos));
                }
                else
                {
                    //Left Clicked
                    _onTileLeftClicked.Invoke(GetIndex(levelData, tileData.xPos, tileData.yPos));
                }
            }

            if (tileData.xPos == 0 && tileData.yPos == 0)
            {
                Rect rect = GUILayoutUtility.GetLastRect();
                _gridDrawPos = new Vector2(rect.x, rect.y + GRID_CELL_SIZE - GRID_CELL_PADDING);
            }

            DrawTileBackground(tileData);
            
            //TODO: Put ordering algorithm here to order the tile elements by priority

            for (int i = 0; i < tileData.TileElementDataList.Count; i++)
            {
                DrawTileElement(tileData.TileElementDataList[i]);
            }
        }

        private void DrawTileBackground(TileData tileData)
        {
            
        }

        private void DrawTileElement(TileElementData tileElementData)
        {
            Rect rect = GUILayoutUtility.GetLastRect();
                
            //TODO: Make this rect drawing sizeable in the future 
            rect = new Rect(rect.x + GRID_CELL_PADDING, rect.y + GRID_CELL_PADDING, rect.width - 2 * GRID_CELL_PADDING,
                rect.height - 2 * GRID_CELL_PADDING);

            Color elementColor = GetColorByType(tileElementData);


            EditorGUI.DrawRect(rect, elementColor);

        }

        private Color GetColorByType(TileElementData tileElementData)
        {
            switch (tileElementData.ElementType)
            {
                case BoardElementType.RedStone:
                    return Color.red;
                case BoardElementType.BlueStone:
                    return Color.blue;
                case BoardElementType.YellowStone:
                    return Color.yellow;
                case BoardElementType.PurpleStone:
                    return Color.magenta;
                case BoardElementType.GreenStone:
                    return Color.green;
                case BoardElementType.StoneSpawner:
                    return Color.black;
                case BoardElementType.RandomStone:
                    return Color.white;
                default:
                    return Color.white;
            }
        }
    }
}