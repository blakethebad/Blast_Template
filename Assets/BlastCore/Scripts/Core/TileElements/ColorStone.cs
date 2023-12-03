using System;
using System.Threading.Tasks;
using DG.Tweening;
using Match3.Grid;
using Match3.Grid.Match;
using Match3.Services.AssetManagement;
using Match3.Tile.Interfaces;
using UnityEngine;

namespace Match3.Tile
{
    public class ColorStone : BaseTileElement, IActivatable, ISwappable, IDroppable
    {
        public override TileLayerType Layer { get; protected set; } = TileLayerType.ItemLayer;
        
        public void Activate(Match activatedMatch, Action onActivationComplete)
        {
            Tile.RemoveElementFromTile(this);
            ActivateVisuals(onActivationComplete);
        }
        
        private async void ActivateVisuals(Action onActivationComplete)
        {
            Transform.DOScale(1.2f, 0.3f);
            await Task.Delay(TimeSpan.FromSeconds(0.4f));
            Transform.localScale = Vector3.one;
            ReturnElementToPool();
            onActivationComplete.Invoke();
        }

        public void Swap(Direction direction) => _propertyHelper.Swap(this, Tile, direction);
        public void Drop(Tile droppedTile, Action onDropComplete) => _propertyHelper.Drop(this, Tile, droppedTile, onDropComplete);
    }
}