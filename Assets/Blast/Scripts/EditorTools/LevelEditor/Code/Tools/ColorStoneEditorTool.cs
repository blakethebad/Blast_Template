using Blast.Core.Grid.GridData;
using UnityEngine;

namespace Blast.EditorTools.LevelEditor.Code.Tools
{
    [CreateAssetMenu(fileName = "Color Stone Editor Tool", menuName = "Tile Element Editor Tools/ColorStone")]
    public class ColorStoneEditorTool : TileElementEditorTool
    {
        public override TileElementData GenerateDuplicateTileElementData()
        {
            TileElementData duplicate = new TileElementData();
            duplicate.ElementType = ElementType;

            return duplicate;
        }
    }
}

