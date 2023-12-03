using UnityEngine;
using UnityEngine.EventSystems;

namespace Match3.Grid.StateController
{
    public interface IGridInputReciever
    {
        public void OnEnter(PointerEventData eventData);
        public void OnDrag(PointerEventData eventData);
        public void OnUp(PointerEventData eventData);
    }
}