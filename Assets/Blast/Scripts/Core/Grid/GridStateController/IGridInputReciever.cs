using UnityEngine.EventSystems;

namespace Blast.Scripts.Core.Grid.GridStateController
{
    public interface IGridInputReciever
    {
        public void OnEnter(PointerEventData eventData);
        public void OnDrag(PointerEventData eventData);
        public void OnUp(PointerEventData eventData);
    }
}