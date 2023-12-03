using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using Match3.Grid;
using Match3.Grid.Match;
using UnityEngine;

namespace Match3.Tile
{
    public class HorizontalBooster : Booster
    {
        public override void Activate(Match activatedMatch, Action onActivationComplete)
        {
            Match boosterMatch = new HorizontalBoosterMatch(MatchType.HorizontalBoosterMatch, Tile);
            Tile.RemoveElementFromTile(this);
            ReturnElementToPool();

            GridMono.OnMatchCreated.Invoke(boosterMatch);
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