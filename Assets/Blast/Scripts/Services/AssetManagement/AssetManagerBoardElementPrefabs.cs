using System.Collections.Generic;
using Blast.Scripts.Services.AssetManagement.AssetGroup;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Blast.Scripts.Services.AssetManagement
{
    public class AssetManagerBoardElementPrefabs
    {
        private Dictionary<BoardElementType, Queue<GameObject>> BoardElementObjectPool;

        private Transform _poolTransform;

        public void InitGamePrefabs(BoardElementAssetGroup assetGroup)
        {
            GameObject _prefabPoolParent = new GameObject("Prefab_Pool");
            Object.DontDestroyOnLoad(_prefabPoolParent);
            _poolTransform = _prefabPoolParent.transform;
            
            BoardElementObjectPool = new Dictionary<BoardElementType, Queue<GameObject>>();
            GeneratePools(assetGroup);
            
        }

        private void GeneratePools(BoardElementAssetGroup assetGroup)
        {
            foreach (var prefabPair in assetGroup.BoardElementAssetDictionary)
            {
                if(prefabPair.Value == null)
                    continue;
                
                BoardElementObjectPool.Add(prefabPair.Key, new Queue<GameObject>());

                for (int i = 0; i < 5; i++)
                {
                    GameObject pooledObject = UnityEngine.Object.Instantiate(prefabPair.Value, Vector3.zero, Quaternion.identity, _poolTransform);
                    pooledObject.SetActive(false);
                    BoardElementObjectPool[prefabPair.Key].Enqueue(pooledObject);
                }
            }
        }


        
        public GameObject GetPrefab(BoardElementType type)
        {
            GameObject prefab = BoardElementObjectPool[type].Dequeue();

            if (BoardElementObjectPool[type].Count <= 1)
            {
                GameObject pooledObject = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, _poolTransform);
                pooledObject.SetActive(false);

                BoardElementObjectPool[type].Enqueue(pooledObject);
            }
            
            return prefab;
        }

        public void ReturnToPool(BoardElementType type, GameObject gameObject) => BoardElementObjectPool[type].Enqueue(gameObject);
    }
}