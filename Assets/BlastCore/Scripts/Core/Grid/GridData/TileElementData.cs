using System;
using Match3.Tile;
using UnityEngine;

namespace Match3.Grid.GridData
{
    [Serializable]
    public abstract class TileElementData
    {
        [SerializeReference] public TileData TileData;

        [SerializeField] private BoardElementType _elementType;
        
        public BoardElementType ElementType
        {
            get { return _elementType; }
            set { _elementType = value; }
        }


        //Used in level creation process to 
        public abstract BaseTileElement GenerateBase();


    }
}