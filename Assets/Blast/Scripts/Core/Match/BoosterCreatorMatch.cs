using System.Collections;
using System.Collections.Generic;
using Blast.Scripts.Core.Grid;
using Blast.Scripts.Core.Grid.GridData;
using UnityEngine;

namespace Blast.Scripts.Core.Match
{
    public class BoosterCreatorMatch : Match
    {
        private readonly WaitForSeconds _creationDelay = new WaitForSeconds(0.1f);
        private readonly Tile.Tile _startTile;
        public BoosterCreatorMatch(MatchType matchType, Tile.Tile startTile, HashSet<Tile.Tile> tiles) : base(matchType)
        {
            MatchedTiles.UnionWith(tiles);
            _startTile = startTile;
        }
        
        public override IEnumerator ExecuteMatch()
        {
            MatchedTiles.Remove(_startTile);
            ActivateTileGroup(MatchedTiles);
            yield return _creationDelay;
            _startTile.Activate(this, () =>
            {
                BoosterData boosterData = new BoosterData();
                boosterData.ElementType = GetBoosterTypeFromMatchType();
                GridMono.OnElementRequested.Invoke(boosterData,_startTile);
                ActivatedTiles.Add(_startTile);
            });
        }
        
        private BoardElementType GetBoosterTypeFromMatchType()
        {
            return MatchType switch
            {
                MatchType.HorizontalMatch => BoardElementType.HorizontalBooster,
                MatchType.VerticalMatch => BoardElementType.VerticalBooster,
                MatchType.SquareMatch => BoardElementType.PlusBooster,
                MatchType.FiveMatch => BoardElementType.VortexBooster,
                _ => BoardElementType.None
            };
        }
    }
}