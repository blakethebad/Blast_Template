using UnityEngine;

namespace Blast.Services.AssetManagement.AssetGroup
{
    public abstract class BaseAssetGroup : ScriptableObject
    {
        [SerializeField] private AssetGroupTag tag;
        public AssetGroupTag Tag => tag;

        public abstract void InitAssetGroup();
    }
}

