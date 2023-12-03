using UnityEngine;

namespace Blast.Scripts.Services
{
    [System.Serializable]
    public class SerializedPair<T1,T2>
    {
        [field: SerializeField] public T1 Key { get; set; }
        [field: SerializeField] public T2 Value { get; set; }
        
        public SerializedPair(T1 t1,T2 t2)
        {
            Key = t1;
            Value = t2;
        }
    }
}