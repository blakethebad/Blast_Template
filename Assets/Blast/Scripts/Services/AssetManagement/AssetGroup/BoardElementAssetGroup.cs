using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blast.Scripts.Services.AssetManagement.AssetGroup
{
    [CreateAssetMenu(menuName = "Asset Group/Board Element Asset Group", fileName = "Board Element Asset Group")]
    public class BoardElementAssetGroup : BaseAssetGroup
    {
        [HideInInspector] public List<SerializedPair<BoardElementType, GameObject>> BoardElementPrefabs;
        [NonSerialized, HideInInspector] public Dictionary<BoardElementType, GameObject> BoardElementAssetDictionary;
        
        public override void InitAssetGroup()
        {
            BoardElementAssetDictionary = new Dictionary<BoardElementType, GameObject>();
            foreach (var prefabPair in BoardElementPrefabs)
            {
                BoardElementAssetDictionary.Add(prefabPair.Key, prefabPair.Value);
            }
        }

        public void UpdateDictionary()
        {
            if (BoardElementPrefabs.Count <= 0)
            {
                foreach (var type in Enum.GetValues(typeof(BoardElementType)))
                {
                    BoardElementPrefabs.Add(new SerializedPair<BoardElementType, GameObject>((BoardElementType)type, null));
                }
                return;
            }

            List<SerializedPair<BoardElementType, GameObject>> newList = new List<SerializedPair<BoardElementType, GameObject>>();

            foreach (var type in Enum.GetValues(typeof(BoardElementType)))
            {
                var currentPair = BoardElementPrefabs.Find((pair => pair.Key == (BoardElementType)type));
                newList.Add(currentPair != null
                    ? new SerializedPair<BoardElementType, GameObject>(currentPair.Key, currentPair.Value)
                    : new SerializedPair<BoardElementType, GameObject>((BoardElementType)type, null));
            }
            BoardElementPrefabs.Clear();
            BoardElementPrefabs.AddRange(newList);

        }
    }
    
}