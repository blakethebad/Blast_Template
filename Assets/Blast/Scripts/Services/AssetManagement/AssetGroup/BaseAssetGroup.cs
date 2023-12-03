using UnityEngine;

namespace Blast.Scripts.Services.AssetManagement.AssetGroup
{
    public abstract class BaseAssetGroup : ScriptableObject
    {
        [SerializeField] private AssetGroupTag tag;
        public AssetGroupTag Tag => tag;

        public abstract void InitAssetGroup();
    }
}

