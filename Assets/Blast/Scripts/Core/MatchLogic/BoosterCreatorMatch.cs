using System.Collections;
using System.Collections.Generic;
using Blast.Core.Grid;
using Blast.Core.Grid.GridData;
using Blast.Core.TileLogic;
using UnityEngine;

namespace Blast.Core.MatchLogic
{
    public class BoosterCreatorMatch : Match
    {
        private readonly WaitForSeconds _creationDelay = new WaitForSeconds(0.1f);
        private readonly Tile _startTile;
        public BoosterCreatorMatch(MatchType matchType, Tile startTile, HashSet<Tile> tiles) : base(matchType)
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