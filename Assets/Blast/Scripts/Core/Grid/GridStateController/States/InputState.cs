using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blast.Scripts.Core.Grid.GridStateController.States
{
    public class InputState: BaseGridState, IGridInputReciever
    {
        private Camera _mainCamera;
        private Vector2 _clickPosition;
        private Vector2Int _worldPosition;
        private Direction _dragDirection;
        private bool _isRecentClick;
        private readonly Action<Vector2Int, Direction> _inputCallback;

        public InputState(Action<StateInfoContainer> changeState, Action<Vector2Int, Direction> inputCallback) : base(changeState)
        {
            _mainCamera = Camera.main;
            _inputCallback = inputCallback;
        }
        
        public override void EnterState(StateInfoContainer stateInfoContainer)
        {
        }

        public void OnEnter(PointerEventData eventData)
        {
            _isRecentClick = true;
            _clickPosition = eventData.position;
            Vector2 screenPosition = _mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, -_mainCamera.transform.position.z));
            _worldPosition.x = Mathf.RoundToInt(screenPosition.x);
            _worldPosition.y = Mathf.RoundToInt(screenPosition.y);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(!_isRecentClick) return;
            if((_clickPosition - eventData.position).sqrMagnitude < 900) return;
            Vector2 dragDirection = (eventData.position - _clickPosition).normalized;
            if (dragDirection.y > -0.707 && dragDirection.y < 0.707)
                _dragDirection = dragDirection.x >= 0 ? Direction.Right : Direction.Left;
            else
                _dragDirection = dragDirection.y >= 0 ? Direction.Top : Direction.Bottom;

            _inputCallback.Invoke(_worldPosition, _dragDirection);
            ChangeState.Invoke(new StateInfoContainer(GridState.DelayInputState));
            _isRecentClick = false;
        }

        public void OnUp(PointerEventData eventData)
        {
            if(!_isRecentClick) return;

            _dragDirection = Direction.None;
            _inputCallback.Invoke(_worldPosition, _dragDirection);
            ChangeState.Invoke(new StateInfoContainer(GridState.DelayInputState));
        }
    }
}