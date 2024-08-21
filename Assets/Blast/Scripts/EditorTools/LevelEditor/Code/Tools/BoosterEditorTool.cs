using Blast.Core.Grid.GridData;
using UnityEngine;

namespace Blast.EditorTools.LevelEditor.Code.Tools
{
    [CreateAssetMenu(fileName = "Booster Editor Tool", menuName = "Tile Element Editor Tools/Booster")]
    public class BoosterEditorTool : TileElementEditorTool
    {
        public override TileElementData GenerateDuplicateTileElementData()
        {
            TileElementData duplicate = new TileElementData();

            duplicate.ElementType = ElementType;

            return duplicate;
        }
    }
}