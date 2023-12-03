using System.Collections;
using System.Collections.Generic;
using Blast.Scripts.Core.Grid;
using UnityEngine;

namespace Blast.Scripts.Core.Match
{
    public class HorizontalBoosterMatch : Match
    {
        private readonly List<HashSet<Tile.Tile>> _orderedActivation = new ();

        private readonly WaitForSeconds _delayForActivation = new WaitForSeconds(0.03f);

        public HorizontalBoosterMatch(MatchType matchType, Tile.Tile originTile) : base(matchType)
        {
            CalculateMatch(originTile);
        }

        public override IEnumerator ExecuteMatch()
        {
            foreach (var activationInOrder in _orderedActivation)
            {
                yield return _delayForActivation;
                
                ActivateTileGroup(activationInOrder);
            }
        }

        private void CalculateMatch(Tile.Tile originTile)
        {
            Tile.Tile leftTile = originTile.GetNeighbor(Direction.Left);
            Tile.Tile rightTile = originTile.GetNeighbor(Direction.Right);

            ActivatedTiles.Add(originTile);
            int activationOrder = 0;
            while (leftTile != null || rightTile != null)
            {
                _orderedActivation.Add(new HashSet<Tile.Tile>());
                if (leftTile != null)
                {
                    _orderedActivation[activationOrder].Add(leftTile);
                    MatchedTiles.Add(leftTile);
                    leftTile = leftTile.GetNeighbor(Direction.Left);
                }

                if (rightTile != null)
                {
                    _orderedActivation[activationOrder].Add(rightTile);
                    MatchedTiles.Add(rightTile);
                    rightTile = rightTile.GetNeighbor(Direction.Right);
                }

                activationOrder++;
            }
        }
    }
}
