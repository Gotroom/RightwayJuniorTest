using UnityEngine;

namespace Gameplay.Helpers
{
    public static class GameAreaHelper
    {

        private static Camera _camera;
        

        static GameAreaHelper()
        {
            _camera = Camera.main;
        }


        public static bool IsInGameplayArea(Transform objectTransform, Bounds objectBounds)
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            var camHalfHeight = _camera.orthographicSize;
            var camHalfWidth = camHalfHeight * _camera.aspect;
            var camPos = _camera.transform.position;
            var topBound = camPos.y + camHalfHeight;
            var bottomBound = camPos.y - camHalfHeight;
            var leftBound = camPos.x - camHalfWidth;
            var rightBound = camPos.x + camHalfWidth;

            var objectPos = objectTransform.position;

            return (objectPos.x - objectBounds.extents.x < rightBound)
                && (objectPos.x + objectBounds.extents.x > leftBound)
                && (objectPos.y - objectBounds.extents.y < topBound)
                && (objectPos.y + objectBounds.extents.y > bottomBound);

        }

        // проверка на возможность перемещать корабль по оси Х
        public static bool IsPlayerAllowedToMove(Transform objectTransform, Bounds objectBounds, float direction)
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            var camHalfHeight = _camera.orthographicSize;
            var camHalfWidth = camHalfHeight * _camera.aspect;
            var camPos = _camera.transform.position;
            var leftBound = camPos.x - camHalfWidth;
            var rightBound = camPos.x + camHalfWidth;

            var objectPos = objectTransform.position;
            
            if (direction < 0)
            {
                return objectPos.x - objectBounds.extents.x > leftBound;
            }
            else
            {
                return objectPos.x + objectBounds.extents.x < rightBound;
            }
        }
    }
}
