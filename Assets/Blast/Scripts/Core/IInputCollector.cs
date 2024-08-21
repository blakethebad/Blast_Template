using UnityEngine;

namespace Blast
{
    public interface IInputCollector
    {
        void ToggleInput();
        void OnGridClick(Vector2 clickPosition);
        void OnPointerUp(Vector2 upPosition);
    }
}