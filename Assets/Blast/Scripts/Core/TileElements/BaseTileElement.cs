using Blast.Core.Grid.GridData;
using Blast.Core.TileLogic;
using UnityEngine;

namespace Blast.Core.TileElements
{
    public abstract class BaseTileElement
    {
        protected Tile Tile { get; private set; }
        public Transform Transform { get; private set; }
        public BoardElementType Type { get; private set; }
        public abstract TileLayerType Layer { get; protected set; }

        protected readonly ElementPropertyHelper _propertyHelper = new ElementPropertyHelper();

        public void InitBaseTileElement(TileElementData tileElementData, Transform transform)
        {
            Type = tileElementData.ElementType;
            Transform = transform;
        }

        public void SetTile(Tile tile) => Tile = tile;

        protected void ReturnElementToPool()
        {
            Transform.gameObject.SetActive(false);
            Transform.localScale = Vector3.one;
            Transform.position = Vector3.zero;
        }
    }
}