using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Grid.Match
{
    public class VerticalBoosterMatch : Match
    {
        private readonly List<HashSet<Tile.Tile>> _orderedActivation = new ();
        private readonly WaitForSeconds _delayForActivation = new WaitForSeconds(0.03f);


        public VerticalBoosterMatch(MatchType matchType, Tile.Tile originTile) : base(matchType)
        {
            CalculateMatch(originTile);
        }

        public override IEnumerator ExecuteMatch()
        {
            foreach (var currentSequence in _orderedActivation)
            {
                yield return _delayForActivation;
                
                ActivateTileGroup(currentSequence);
            }
        }
        
        private void CalculateMatch(Tile.Tile originTile)
        {
            Tile.Tile topTile = originTile.GetNeighbor(Direction.Top);
            Tile.Tile bottomTile = originTile.GetNeighbor(Direction.Bottom);

            ActivatedTiles.Add(originTile);
            int activationOrder = 0;
            while (topTile != null || bottomTile != null)
            {
                _orderedActivation.Add(new HashSet<Tile.Tile>());
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