using System.Collections.Generic;
using Blast.Services.AssetManagement.AssetGroup;
using UnityEngine;

namespace Blast.Services.AssetManagement
{
    public enum AssetGroupTag
    {
        Levels,
        BoardElements
    }
    
    public static class AssetManager
    {
        public static AssetManagerLevelEditor LevelEditor { get; set; } = new AssetManagerLevelEditor();
        public static AssetManagerLevels Levels { get; private set; } = new AssetManagerLevels();
        public static AssetManagerBoardElementPrefabs Prefabs { get; private set; } = new AssetManagerBoardElementPrefabs();

        public static void InitAssetManagement(List<BaseAssetGroup> assetGroups)
        {
            Debug.Log("Initialized");
            foreach (BaseAssetGroup assetGroup in assetGroups)
            {
                assetGroup.InitAssetGroup();
                
                if (assetGroup.Tag == AssetGroupTag.Levels)
                {
                    Levels.InitializeLevelAssets((LevelAssetGroup) assetGroup);
                }

                if (assetGroup.Tag == AssetGroupTag.BoardElements)
                {
                    Debug.Log("Board Elements");
                    Prefabs.InitGamePrefabs((BoardElementAssetGroup) assetGroup);
                }
            }
        }
    }
}