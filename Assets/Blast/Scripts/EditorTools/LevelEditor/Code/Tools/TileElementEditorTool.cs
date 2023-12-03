using Blast.Core.Grid.GridData;
using UnityEngine;

namespace Blast.EditorTools.LevelEditor.Code.Tools
{
    public abstract class TileElementEditorTool : ScriptableObject
    {
        public string Name => this.name;

        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;

        [SerializeField] private string tooltip;
        public string ToolTip => tooltip;

        [SerializeReference] public BoardElementType elementType;
        public BoardElementType ElementType => elementType;

        public abstract TileElementData GenerateDuplicateTileElementData();
    }
}