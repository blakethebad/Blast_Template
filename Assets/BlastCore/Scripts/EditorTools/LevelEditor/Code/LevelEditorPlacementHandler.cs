using System.Collections.Generic;
using System.Dynamic;
using Match3.Grid.GridData;
using Match3.LevelEditor.Tools;
using UnityEngine;
using Object = System.Object;

namespace Match3.LevelEditor
{
    public class LevelEditorPlacementHandler
    {
        private LevelEditorWindow _levelEditorWindow;

        public LevelEditorPlacementHandler(LevelEditorWindow levelEditorWindow)
        {
            _levelEditorWindow = levelEditorWindow;
        }
        
        public void OnTileLeftClicked(LevelData levelData, int tileDataIndex)
        {
            if (_levelEditorWindow.SelectedTool)
            {
                _levelEditorWindow.SetSelectedTile(null);

                PlaceTileElement(levelData, tileDataIndex);

            }
            else
            {
                _levelEditorWindow.SetSelectedTile(levelData.TileDataList[tileDataIndex]);
            }
        }

        private void PlaceTileElement(LevelData levelData, int tileDataIndex)
        {
            TileElementEditorTool tool = _levelEditorWindow.SelectedTool;
            
            levelData.TileDataList[tileDataIndex].TileElementDataList.Clear();

            TileElementData newElement = tool.GenerateDuplicateTileElementData();

            newElement.TileData = levelData.TileDataList[tileDataIndex];

            levelData.TileDataList[tileDataIndex].TileElementDataList.Add(newElement);

        }

        public void OnTileRightClicked(LevelData levelData, int tileDataIndex)
        {
            int xPos = levelData.TileDataList[tileDataIndex].xPos;
            int yPos = levelData.TileDataList[tileDataIndex].yPos;
            
            levelData.TileDataList[tileDataIndex] = new TileData();
            levelData.TileDataList[tileDataIndex].xPos = xPos;
            levelData.TileDataList[tileDataIndex].yPos = yPos;
            levelData.TileDataList[tileDataIndex].TileElementDataList = new List<TileElementData>();
            levelData.TileDataList[tileDataIndex].TileElementDataList.Add(new ColorStoneData()
            {
                ElementType = BoardElementType.RandomStone
            });
        }

        private void PlaceColorStone(LevelData levelData, int tileDataIndex)
        {
            TileElementEditorTool itemTool = _levelEditorWindow.SelectedTool;
            
            TileElementData tileElementData = levelData.TileDataList[tileDataIndex].TileElementDataList.Find((data => data is ColorStoneData));

            if (tileElementData != null)
            {
                levelData.TileDataList[tileDataIndex].TileElementDataList.Remove(tileElementData);
            }

            ColorStoneData colorStoneData = new ColorStoneData();

            colorStoneData.ElementType = itemTool.ElementType;
            colorStoneData.TileData = levelData.TileDataList[tileDataIndex];
            
            levelData.TileDataList[tileDataIndex].TileElementDataList.Add(colorStoneData);
        }

       
    }
}