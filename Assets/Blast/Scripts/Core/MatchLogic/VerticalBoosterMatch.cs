using System.Collections;
using System.Collections.Generic;
using Blast.Core.Grid;
using Blast.Core.TileLogic;
using UnityEngine;

namespace Blast.Core.MatchLogic
{
    public class VerticalBoosterMatch : Match
    {
        private readonly List<HashSet<Tile>> _orderedActivation = new ();
        private readonly WaitForSeconds _delayForActivation = new WaitForSeconds(0.03f);


        public VerticalBoosterMatch(MatchType matchType, Tile originTile) : base(matchType)
        {
            CalculateMatch(originTile);
        }

        public override void ExecuteMatch()
        {
            foreach (var currentSequence in _orderedActivation)
            {
                ActivateTileGroup(currentSequence);
            }
        }
        
        private void CalculateMatch(Tile originTile)
        {
            Tile topTile = originTile.GetNeighbor(Direction.Top);
            Tile bottomTile = originTile.GetNeighbor(Direction.Bottom);

            ActivatedTiles.Add(originTile);
            int activationOrder = 0;
            while (topTile != null || bottomTile != null)
            {
                _orderedActivation.Add(new HashSet<Tile>());
                if (topTile != null)
                {
                    _orderedActivation[activationOrder].Add(topTile);
                    MatchedTiles.Add(topTile);
                    topTile = topTile.GetNeighbor(Direction.Top);
                }

                if (bottomTile != null)
                {
                    _orderedActivation[activationOrder].Add(bottomTile);
                    MatchedTiles.Add(bottomTile);
                    bottomTile = bottomTile.GetNeighbor(Direction.Bottom);
                }
            }
        }
    }
}