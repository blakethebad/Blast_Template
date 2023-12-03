using System;
using System.Threading.Tasks;
using Blast.Scripts.Core.Grid;
using Blast.Scripts.Core.TileElements;
using Blast.Scripts.Core.TileElements.Interfaces;

namespace Blast.Scripts.Core.Tile.TileStates
{
    public class RecieveInputState : BaseTileState
    {
        public override TileState State { get; protected set; } = TileState.RecieveInputState;

        public RecieveInputState(Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
        {
        }
        
        public override bool CanTranslateTo(TileState state) => state is TileState.ActivateState or TileState.IdleState;

        public override void EnterState(TileStatePackage tileStatePackage)
        {
            if (tileStatePackage.InputDirection == Direction.None && !CoreTile.IsEmpty() && CoreTile.GetFirstElement() is IClickActivatable clickActivatable)
            {
                clickActivatable.Activate(null, () =>
                {
                    ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
                });
                return;
            }

            Tile neighbor = CoreTile.GetNeighbor(tileStatePackage.InputDirection);

            if (CoreTile.IsEmpty() ||
                CoreTile.GetFirstElement() is not ISwappable inputElement ||
                neighbor?.GetFirstElement() is not ISwappable swappedElement)
            {
                ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
                return;
            }

            if (BoardElementHelper.IsBaseType(((BaseTileElement)inputElement).Type, BaseElementType.Booster) &&
                BoardElementHelper.IsBaseType(((BaseTileElement)swappedElement).Type, BaseElementType.Booster))
            {
                BoosterCombo();
            }
            else
            {
                RegularSwap(inputElement, swappedElement, tileStatePackage);
            }
            

        }
        
        private void BoosterCombo()
        {
            
        }

        private void RegularSwap(ISwappable inputElement, ISwappable swappedElement, TileStatePackage statePackage)
        {
            inputElement.Swap(statePackage.InputDirection);
            swappedElement.Swap(DirectionHelper.Opposite(statePackage.InputDirection));
            
            bool foundInputMatch = CheckOnTile(CoreTile, swappedElement, inputElement);
            bool foundSwapMatch = CheckOnTile(CoreTile.GetNeighbor(statePackage.InputDirection), inputElement, swappedElement);
            
            if (!foundInputMatch && !foundSwapMatch)
                Undo();

            if(!foundInputMatch) 
                ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
            
            async void Undo()
            {
                await Task.Delay(TimeSpan.FromSeconds(0.2f));
                
                inputElement.Swap(DirectionHelper.Opposite(statePackage.InputDirection));
                swappedElement.Swap(statePackage.InputDirection);
            }
        }
        

        private bool CheckOnTile(Tile tileToCheck, ISwappable swappable, ISwappable swappedElement)
        {
            if (swappable is not IBooster booster) return tileToCheck.CheckAndActivateMatch();
            
            booster.SwapActivate((BaseTileElement)swappedElement, () =>
            {
                ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
            });
            return true;

        }
    }
}