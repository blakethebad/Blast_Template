using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Services.AssetManagement.AssetGroup
{
    public abstract class BaseAssetGroup : ScriptableObject
    {
        [SerializeField] private AssetGroupTag tag;
        public AssetGroupTag Tag => tag;

        public abstract void InitAssetGroup();
    }
}

