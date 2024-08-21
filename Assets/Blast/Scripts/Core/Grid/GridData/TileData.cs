using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blast.Core.Grid.GridData
{
    [Serializable]
    public class TileElementData
    {
        [SerializeReference] public TileData TileData;

        [SerializeField] private BoardElementType _elementType;
        
        public BoardElementType ElementType
        {
            get { return _elementType; }
            set { _elementType = value; }
        }
    }
    
    [Serializable]
    public class TileData
    {
        [field: SerializeField] public bool IsBottomTile { get; set; }

        public int xPos;
        public int yPos;

        [SerializeReference] public List<TileElementData> TileElementDataList;
    }
}