using System.Linq;
using Blast.Core.Grid.GridData;
using Blast.Services.AssetManagement.AssetGroup;
using UnityEngine;

namespace Blast.Services.AssetManagement
{
    public class AssetManagerLevels
    {
        private LevelAssetGroup _levelAssetGroup;

        public void InitializeLevelAssets(LevelAssetGroup levelAssetGroup)
        {
            _levelAssetGroup = levelAssetGroup;
        }

        public LevelData GetLevel(int levelIndex)
        {
            LevelData levelData;

            if (levelIndex <= 0)
                levelData = _levelAssetGroup.Levels.Min();
            if (levelIndex > _levelAssetGroup.Levels.Count)
                levelData = _levelAssetGroup.Levels.Max();
            else
                levelData = _levelAssetGroup.Levels.Find((level => level.levelIndex == levelIndex));

            return levelData;
        }

        public int GetLevelCount() => _levelAssetGroup.Levels.Count;
    }
}