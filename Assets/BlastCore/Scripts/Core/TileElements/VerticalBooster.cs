using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using Match3.Grid;
using Match3.Grid.Match;
using Match3.Tile;
using UnityEngine;

namespace Match3.Tile
{
    public class VerticalBooster : Booster
    {
        private Match _boosterMatch;
        public override void Activate(Match activatedMatch, Action onActivationComplete)
        {
            _boosterMatch = new VerticalBoosterMatch(MatchType.VerticalBoosterMatch, Tile);
            Tile.RemoveElementFromTile(this);
            ReturnElementToPool();

            GridMono.OnMatchCreated.Invoke(_boosterMatch);
            onActivationComplete.Invoke();
            
        }

        public override void ComboActivate(BaseTileElement swappedElement, Action onBoosterActivated)
        {
            
        }

        public override void SwapActivate(BaseTileElement swappedElement, Action onBoosterActivated)
        {
            Activate(null, onBoosterActivated);
        }
    }
}