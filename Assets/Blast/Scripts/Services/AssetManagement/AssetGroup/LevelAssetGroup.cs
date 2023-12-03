using System.Collections.Generic;
using Blast.Scripts.Core.Grid.GridData;
using UnityEngine;

namespace Blast.Scripts.Services.AssetManagement.AssetGroup
{
    [CreateAssetMenu(menuName = "Asset Group/Level Asset Group", fileName = "Level Assets")]
    public class LevelAssetGroup : BaseAssetGroup
    {
        [SerializeField] private List<LevelData> levels;
        public List<LevelData> Levels => levels;
        public override void InitAssetGroup()
        {
        }
    }
}