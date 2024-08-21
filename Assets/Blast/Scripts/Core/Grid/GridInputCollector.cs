using System;
using UnityEngine;

namespace Blast.Core
{
    public class GridInputCollector : IInputCollector
    {
        private bool _isInputEnabled = true;
        private Camera _mainCamera;
        private Vector2Int _worldPosition;
        private Action<Vector2Int> _inputCallback;

        public GridInputCollector(Action<Vector2Int> onClickCollected, Camera mainCamera)
        {
            _inputCallback = onClickCollected;
            _mainCamera = mainCamera;
        }
        
        public void ToggleInput()
        {
            _isInputEnabled = !_isInputEnabled;
        }

        public void OnGridClick(Vector2 clickPosition)
        {
            if(!_isInputEnabled)
                return;
            
            Vector2 screenPosition = _mainCamera.ScreenToWorldPoint(new Vector3(clickPosition.x, clickPosition.y, -_mainCamera.transform.position.z));
            _worldPosition.x = Mathf.RoundToInt(screenPosition.x);
            _worldPosition.y = Mathf.RoundToInt(screenPosition.y);
        }

        public void OnPointerUp(Vector2 upPosition)
        {
            _inputCallback.Invoke(_worldPosition);
        }
    }
}