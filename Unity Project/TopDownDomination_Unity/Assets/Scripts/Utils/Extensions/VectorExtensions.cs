using UnityEngine;

namespace Utils.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 RandomPositionInsideRect(this RectTransform targetRectTransform)
        {
            var position = targetRectTransform.position;
            var panelSize = targetRectTransform.rect.size;

            var minX = position.x - panelSize.x / 2f;
            var maxX = position.x + panelSize.x / 2f;
            var minY = position.y - panelSize.y / 2f;
            var maxY = position.y + panelSize.y / 2f;

            var randomX = Random.Range(minX, maxX);
            var randomY = Random.Range(minY, maxY);

            return new Vector3(randomX, randomY, position.z);
        }

        public static Vector3 Multiply(this Vector3 targetVector, Vector3 multiply)
        {
            return new Vector3
            (
                targetVector.x * multiply.x,
                targetVector.y * multiply.y,
                targetVector.z * multiply.z
            );
        }

        public static Vector3 Set(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }
        
        public static Vector3 SetX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }
        
        public static Vector3 SetY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }
        
        public static Vector3 SetZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector2 Set(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(x ?? vector.x, y ?? vector.y);
        }
        
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
        
        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0f);
        }
        
        public static Vector3 Abs(this Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }

        public static Vector2 Abs(this Vector2 vector)
        {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }
        
        public static Vector3 Round(this Vector3 vector)
        {
            return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
        }

        public static Vector2 Round(this Vector2 vector)
        {
            return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
        }
    }
}