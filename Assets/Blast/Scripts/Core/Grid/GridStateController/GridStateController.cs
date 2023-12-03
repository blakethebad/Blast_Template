using Blast.Scripts.Core.Grid.GridData;
using Blast.Scripts.Core.Grid.GridStateController.States;
using UnityEngine.EventSystems;

namespace Blast.Scripts.Core.Grid.GridStateController
{
    public sealed class GridStateController : IGridInputReciever
    {
        private BaseGridState _inputState;
        private BaseGridState _delayInputState;
        private BaseGridState _winFailState;
        private BaseGridState _gameOverState;
        
        private BaseGridState _currentState;

        public void StartStateController(LevelData currentLevelData, GridMono gridMono)
        {
            _inputState = new InputState(ChangeState, gridMono.SendInputToPosition);
            _delayInputState = new DelayInputState(ChangeState);
            
            _currentState = _inputState;
            ChangeState(new StateInfoContainer(GridState.InputState));
        }

        private void ChangeState(StateInfoContainer stateInfo)
        {
            if(!_currentState.CanChangeState(stateInfo))
                return;

            _currentState.ExitState(stateInfo);

            _currentState = stateInfo.State switch
            {
                GridState.InputState => _inputState,
                GridState.DelayInputState => _delayInputState,
                GridState.WinFailState => _winFailState,
                GridState.GameOverState => _gameOverState,
                _ => _currentState
            };

            _currentState.EnterState(stateInfo);
        }

        public void OnEnter(PointerEventData eventData)
        {
            if (_currentState is IGridInputReciever pointerDownHandler)
                pointerDownHandler.OnEnter(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_currentState is IGridInputReciever pointerDragHandler)
                pointerDragHandler.OnDrag(eventData);
        }

        public void OnUp(PointerEventData eventData)
        {
            if (_currentState is not IGridInputReciever pointerUpHandler) return; 
            pointerUpHandler.OnUp(eventData);
        }
    }
}