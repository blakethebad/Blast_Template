using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blast.Scripts.Core.Grid.GridData
{
    [Serializable]
    public class TileData
    {
        [field: SerializeField] public bool IsBottomTile { get; set; }

        public int xPos;
        public int yPos;

        [SerializeReference] public List<TileElementData> TileElementDataList;
    }
}