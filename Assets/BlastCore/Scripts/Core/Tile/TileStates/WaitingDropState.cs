using System;
using System.Collections;
using System.Threading.Tasks;
using Match3.Grid.Match;
using Match3.Tile;
using Match3.Tile.TileStates;
using UnityEngine;

namespace Match3.Tile.TileStates
{
    public class WaitingDropState : BaseTileState
    {
        public WaitingDropState(Match3.Tile.Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
        {
        }

        public override TileState State { get; protected set; } = TileState.WaitingDropState;
        public override void EnterState(TileStatePackage tileStatePackage)
        {
        }

        public override bool CanTranslateTo(TileState state) => state is TileState.DropState;
    }
}