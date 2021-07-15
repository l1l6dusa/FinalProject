 
    using UnityEngine;

    public static class Helper
    {
        public static float CalculateInverseAngle(GameObject obj, Vector3 pos)
        {
            var angleRad = Mathf.Atan2(pos.x,pos.z) + Mathf.PI;
            return angleRad;
        }
        
    }
