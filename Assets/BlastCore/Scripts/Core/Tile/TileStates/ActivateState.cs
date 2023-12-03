﻿using System;
using Match3.Grid;
using Match3.Tile.Interfaces;
using UnityEngine;

namespace Match3.Tile.TileStates
{
    public class ActivateState : BaseTileState
    {
        public override TileState State { get; protected set; } = TileState.ActivateState;
        public ActivateState(Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
        {
        }
        
        public override void EnterState(TileStatePackage tileStatePackage)
        {
            if (CoreTile.GetFirstElement() == null || CoreTile.GetFirstElement() is not IActivatable activatable)
            {
                OnActivationComplete();
                return;
            }

            activatable.Activate(tileStatePackage.ActivatedMatch, OnActivationComplete);

            void OnActivationComplete()
            {
                ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
                tileStatePackage.OnTileActivated.Invoke();
            }
        }

        public override bool CanTranslateTo(TileState state) => state is TileState.RefillState or TileState.IdleState;
    }
}