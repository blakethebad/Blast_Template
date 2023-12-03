using System;
using System.Collections.Generic;
using System.Linq;
using Blast.Core.Grid.GridData;
using Blast.EditorTools.LevelEditor.Code.Tools;
using UnityEditor;
using UnityEngine;

namespace Blast.Services.AssetManagement
{
    public class AssetManagerLevelEditor
    {
        public List<LevelInfo> LevelInfos { get; private set; } = new List<LevelInfo>();

        public string[] LevelDisplayNames { get; private set; }


        public T[] GetLevelEditorTileElementTools<T>(string slug) where T : TileElementEditorTool
        {
            string[] assetGuiIDs = AssetDatabase.FindAssets($"t:{typeof(T).FullName}", 
                new[] { AssetPathProvider.BLAST_LEVEL_ELEMENTS_PATH + slug });
            
            T[] tileElementTools = new T[assetGuiIDs.Length];

            for (int i = 0; i < tileElementTools.Length; i++)
            {
                tileElementTools[i] = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assetGuiIDs[i]));
            }

            return tileElementTools;
        }

        public Texture2D GetIcon(string slug)
        {
            UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(AssetPathProvider.BLAST_LEVEL_ICON_PATH + slug + "." + "png");

            foreach (var asset in assets)
            {
                if (asset is Texture2D texture2D)
                    return texture2D;
            }

            return null;
        }
        
        public void RefreshLevelInfoList()
        {
            List<LevelInfo> levelInfos = new List<LevelInfo>();

            string directory = AssetPathProvider.BLAST_LEVEL_PATH + System.IO.Path.DirectorySeparatorChar;

            string[] levelDataGuiIds = AssetDatabase.FindAssets("t:" + nameof(LevelData), new[] { directory });

            foreach (string guiId in levelDataGuiIds)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guiId);
                LevelData levelData = AssetDatabase.LoadAssetAtPath<LevelData>(assetPath);
                levelInfos.Add(new LevelInfo(levelData.LevelId, guiId, levelData.name, levelData.levelIndex));

            }

            LevelInfos = levelInfos.OrderBy(info => info.LevelNumber).ToList();
            
            LevelDisplayNames = new string[LevelInfos.Count];
            for (int i = 0; i < LevelDisplayNames.Length; i++)
                LevelDisplayNames[i] = LevelInfos[i].FileName;
        }

        public void CreateNewLevelAsset()
        {
            LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
            levelData.LevelId = CreateUniqueId;

            int newLevelIndex;
            if (LevelInfos != null && LevelInfos.Count > 0)
                newLevelIndex = LevelInfos.Max(max => max.LevelNumber) + 1;
            else
                newLevelIndex = 1;

            levelData.levelIndex = newLevelIndex;

            ResetLevelDataWithNewSize(levelData, 5, 5);
            
            string newAssetPath = AssetPathProvider.BLAST_LEVEL_PATH + System.IO.Path.DirectorySeparatorChar + newLevelIndex.ToString("0000000") + ".asset";

            if (!System.IO.File.Exists(newAssetPath))
                AssetDatabase.CreateAsset(levelData, newAssetPath);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            RefreshLevelInfoList();
        }

        public void ResetLevelDataWithNewSize(LevelData levelData, in int sizeX, in int sizeY)
        {
            levelData.TileDataList = new List<TileData>();

            levelData.sizeX = sizeX;
            levelData.sizeY = sizeY;
            
            for (int y = 0; y < levelData.sizeY; y++)
            {
                for (int x = 0; x < levelData.sizeX; x++)
                {
                    TileData tileData = new TileData();
                    tileData.xPos = x;
                    tileData.yPos = y;

                    if (y == 0) tileData.IsBottomTile = true;

                    tileData.TileElementDataList = new List<TileElementData>();

                    //Means a default spawner position
                    if (y == levelData.sizeY - 1)
                    {
                        SpawnerData spawnerData = new SpawnerData();
                        spawnerData.ElementType = BoardElementType.StoneSpawner;
                        spawnerData.TileData = tileData;
                        tileData.TileElementDataList.Add(spawnerData);
                    }
                    else
                    {
                        ColorStoneData randomStone = new ColorStoneData();
                        randomStone.ElementType = BoardElementType.RandomStone;
                        randomStone.TileData = tileData;

                        tileData.TileElementDataList.Add(randomStone);
                    }

                    levelData.TileDataList.Add(tileData);

                }
            }
        }
        
        public bool TryGetLevelInfoByLevelNumber(int levelNumber, out LevelInfo levelInfo, List<LevelInfo> levelInfos = null)
        {
            IEnumerable<LevelInfo> vals = null;
            if(levelInfos == null)
                vals = LevelInfos.Where(x => x.LevelNumber == levelNumber);
            else
                vals = levelInfos.Where(x => x.LevelNumber == levelNumber);

            levelInfo = vals.ElementAtOrDefault(0);

            return (levelInfo != default);
        }
        
        
        public string CreateUniqueId => System.Guid.NewGuid().ToString("N").ToUpper();

        public void SaveLevel(LevelData newLevelData, LevelData SelectedLevelData)
        {
            if (AssetManager.LevelEditor.TryGetLevelInfoByLevelNumber(newLevelData.levelIndex, out LevelInfo levelInfo))
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(levelInfo.GuiId));
            }
            
            string newAssetPath = AssetPathProvider.BLAST_LEVEL_PATH + System.IO.Path.DirectorySeparatorChar + newLevelData.levelIndex.ToString("0000000") + ".asset";

            string assetPath = AssetDatabase.GetAssetPath(SelectedLevelData.GetInstanceID());

            AssetDatabase.DeleteAsset(assetPath);

            if (newLevelData.LevelId == String.Empty)
                newLevelData.LevelId = AssetManager.LevelEditor.CreateUniqueId;

            AssetDatabase.CreateAsset(newLevelData, newAssetPath);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetManager.LevelEditor.RefreshLevelInfoList();
        }

    }
    
    public sealed class LevelInfo
    {
        public string LevelID { get; private set; }
        public string GuiId { get; private set; }
        public string FileName { get; private set; }
        public int LevelNumber { get; private set; }

        public LevelInfo(string levelId, string guiId, string fileName, int levelNumber)
        {
            LevelID = levelId;
            GuiId = guiId;
            FileName = fileName;
            LevelNumber = levelNumber;
        }
    }
}