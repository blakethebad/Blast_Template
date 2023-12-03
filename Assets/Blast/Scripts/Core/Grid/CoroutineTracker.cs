using System.Collections;
using UnityEngine;

namespace Blast.Scripts.Core.Grid
{
    public static class CoroutineTracker
    {
        private static MonoBehaviour _grid;
        public static void InitializeCoroutineTracker(MonoBehaviour gridMono)
        {
            _grid = gridMono;
        }

        public static void StartCoroutine(IEnumerator enumerator)
        {
            _grid.StartCoroutine(enumerator);
        }
    }
}