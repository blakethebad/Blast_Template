using Match3.Grid.GridData;
using Unity.VisualScripting;
using UnityEngine;

namespace Match3.LevelEditor.Tools
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