using UnityEngine;

namespace YusamPackage
{
    public static class MouseHelper
    {
        public static Vector3 GetMouseLookPositionByRay(Vector3 lookPosition, Vector3 inputMousePosition, Camera camera, float distance, int layerMask)
        {
            var ray = camera.ScreenPointToRay(inputMousePosition);

            if (Physics.Raycast(ray, out var hit, distance, layerMask))
            {
                lookPosition = hit.point;
            }
            
            return lookPosition;
        }
        
        public static Vector3 GetMouseLookPositionByRay(Vector3 lookPosition, Vector3 inputMousePosition, Camera camera, float distance)
        {
            var ray = camera.ScreenPointToRay(inputMousePosition);

            if (Physics.Raycast(ray, out var hit, distance))
            {
                lookPosition = hit.point;
            }
            
            return lookPosition;
        }      
    }
}