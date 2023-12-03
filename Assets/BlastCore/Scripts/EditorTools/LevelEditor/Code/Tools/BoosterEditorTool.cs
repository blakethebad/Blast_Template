using Match3.Grid.GridData;
using UnityEngine;

namespace Match3.LevelEditor.Tools
{
    [CreateAssetMenu(fileName = "Booster Editor Tool", menuName = "Tile Element Editor Tools/Booster")]
    public class BoosterEditorTool : TileElementEditorTool
    {
        public override TileElementData GenerateDuplicateTileElementData()
        {
            TileElementData duplicate = new BoosterData();

            duplicate.ElementType = ElementType;

            return duplicate;
        }
    }
}