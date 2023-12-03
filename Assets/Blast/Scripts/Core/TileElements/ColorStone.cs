using System;
using System.Threading.Tasks;
using Blast.Scripts.Core.Grid;
using Blast.Scripts.Core.TileElements.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Blast.Scripts.Core.TileElements
{
    public class ColorStone : BaseTileElement, IActivatable, ISwappable, IDroppable
    {
        public override TileLayerType Layer { get; protected set; } = TileLayerType.ItemLayer;
        
        public void Activate(Match.Match activatedMatch, Action onActivationComplete)
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
        public void Drop(Tile.Tile droppedTile, Action onDropComplete) => _propertyHelper.Drop(this, Tile, droppedTile, onDropComplete);
    }
}