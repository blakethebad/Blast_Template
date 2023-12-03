using System;
using System.Collections.Generic;
using Blast.Scripts.Core.Grid;
using Blast.Scripts.Core.Match;
using Blast.Scripts.Core.Tile.TileStates;

namespace Blast.Scripts.Core.Tile
{
    public sealed class TileMatchChecker
    {
        private Tile _coreTile;
        private Dictionary<MatchType, Func<Match.Match>> _matchCreators;
        private readonly HashSet<Tile> _potentialMatchTiles = new HashSet<Tile>();
        
        private Direction[] _primaryDirections = new [] { Direction.Top , Direction.Left, Direction.Bottom, Direction.Right};

        public void InitMatchChecker(Tile coreTile)
        {
            _coreTile = coreTile;
            _matchCreators = new Dictionary<MatchType, Func<Match.Match>>()
            {
                { MatchType.ThreeMatch , CheckThreeMatch},
                { MatchType.VerticalMatch , CheckVerticalMatch },
                { MatchType.HorizontalMatch , CheckHorizontalMatch },
                { MatchType.SquareMatch , CheckSquareMatch },
                { MatchType.FiveMatch, CheckFiveMatch }
            };
        }
        
        public bool CheckMatch()
        {
            MatchType[] prioritizedMatches = new [] { MatchType.FiveMatch, MatchType.HorizontalMatch , MatchType.VerticalMatch, MatchType.SquareMatch, MatchType.ThreeMatch};

            foreach (MatchType matchType in prioritizedMatches)
            {
                _potentialMatchTiles.Clear();
                _potentialMatchTiles.Add(_coreTile);
                Match.Match match = _matchCreators[matchType].Invoke();

                if (match is not null)
                {
                    GridMono.OnMatchCreated.Invoke(match);
                    return true;
                }
            }

            return false;
        }
        private Match.Match CheckThreeMatch()
        {
            CheckDirection(Direction.Top);
            CheckDirection(Direction.Bottom);
            if (_potentialMatchTiles.Count >= 3)
                return new BasicMatch(MatchType.ThreeMatch, _potentialMatchTiles);

            _potentialMatchTiles.Clear();
            _potentialMatchTiles.Add(_coreTile);
            
            CheckDirection(Direction.Left);
            CheckDirection(Direction.Right);
            return _potentialMatchTiles.Count >= 3 ? new BasicMatch(MatchType.ThreeMatch, _potentialMatchTiles) : null;
        }

        private Match.Match CheckHorizontalMatch()
        {
            CheckDirection(Direction.Right);
            CheckDirection(Direction.Left);
            return _potentialMatchTiles.Count >= 4 ? new BoosterCreatorMatch(MatchType.HorizontalMatch, _coreTile, _potentialMatchTiles) : null;
        }

        private Match.Match CheckVerticalMatch()
        {
            CheckDirection(Direction.Top);
            CheckDirection(Direction.Bottom);
            return _potentialMatchTiles.Count >= 4 ? new BoosterCreatorMatch(MatchType.VerticalMatch, _coreTile, _potentialMatchTiles) : null;
        }

        private Match.Match CheckSquareMatch()
        {
            Tile currentTile = _coreTile;
            foreach (Direction direction in _primaryDirections)
            {
                if (!AvailableForMatch(currentTile, direction) ||
                    !AvailableForMatch(currentTile, DirectionHelper.ClockwiseDirection(direction)) ||
                    !AvailableForMatch(currentTile, DirectionHelper.ClockwiseDirection(DirectionHelper.ClockwiseDirection(direction)))) continue;
                
                _potentialMatchTiles.Add(currentTile.GetNeighbor(direction));
                _potentialMatchTiles.Add(currentTile.GetNeighbor(DirectionHelper.ClockwiseDirection(direction)));
                _potentialMatchTiles.Add(currentTile.GetNeighbor(DirectionHelper.ClockwiseDirection(DirectionHelper.ClockwiseDirection(direction))));
                return new BoosterCreatorMatch(MatchType.SquareMatch, _coreTile, _potentialMatchTiles);
            }
            return null;
        }

        private Match.Match CheckFiveMatch()
        {
            CheckDirection(Direction.Right);
            CheckDirection(Direction.Left);
            if (_potentialMatchTiles.Count >= 5)
                return new BoosterCreatorMatch(MatchType.FiveMatch, _coreTile, _potentialMatchTiles);

            _potentialMatchTiles.Clear();
            _potentialMatchTiles.Add(_coreTile);
            
            CheckDirection(Direction.Top);
            CheckDirection(Direction.Bottom);
            return _potentialMatchTiles.Count >= 5 ? new BoosterCreatorMatch(MatchType.FiveMatch, _coreTile, _potentialMatchTiles) : null;
        }

        private void CheckDirection(Direction direction)
        {
            Tile currentTile = _coreTile;
            while (AvailableForMatch(currentTile, direction))
            {
                currentTile = currentTile.GetNeighbor(direction);
                _potentialMatchTiles.Add(currentTile);
            }
        }

        private bool AvailableForMatch(Tile currentTile, Direction direction)
        {
            if (currentTile.GetNeighbor(direction) == null || !currentTile.GetNeighbor(direction)
                    .LayerContainsType(TileLayerType.ItemLayer,
                        currentTile.GetFirstTypeFromLayer(TileLayerType.ItemLayer))) return false;

            return currentTile.GetNeighbor(direction).CheckCurrentState(TileState.IdleState);
        }
    }
}