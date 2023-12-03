using System;
using Blast.Core.TileElements;
using UnityEngine;

namespace Blast.Core.Grid.GridData
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