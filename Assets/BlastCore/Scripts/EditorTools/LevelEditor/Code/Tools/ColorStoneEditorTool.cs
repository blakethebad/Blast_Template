using System.Collections;
using System.Collections.Generic;
using Match3.Grid.GridData;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Match3.LevelEditor.Tools
{
    [CreateAssetMenu(fileName = "Color Stone Editor Tool", menuName = "Tile Element Editor Tools/ColorStone")]
    public class ColorStoneEditorTool : TileElementEditorTool
    {
        public override TileElementData GenerateDuplicateTileElementData()
        {
            TileElementData duplicate = new ColorStoneData();
            duplicate.ElementType = ElementType;

            return duplicate;
        }
    }
}

