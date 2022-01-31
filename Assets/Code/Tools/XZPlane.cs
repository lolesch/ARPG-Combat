using UnityEngine;

namespace ARPG.Tools
{
    /// <summary>
    /// Use this to calculate Vectors, Directions and such on the XZ-Plane.
    /// The Vector3.y value of an incoming Vector3 is set to 0. A Vector2.y value is converted into a Vector3.z
    /// </summary>
    public static class XZPlane
    {
        public static Vector3 Vector(Vector2 vector) => new Vector3(vector.x, 0, vector.y);
        public static Vector3 Vector(Vector3 vector) => new Vector3(vector.x, 0, vector.z);
        public static Vector3 Vector(Vector3 to, Vector3 from) => Vector(to - from);

        public static float Magnitude(Vector2 vector) => Vector(vector).magnitude;
        public static float Magnitude(Vector3 vector) => Vector(vector).magnitude;
        public static float Magnitude(Vector3 to, Vector3 from) => Vector(to, from).magnitude;

        public static Vector3 Direction(Vector2 vector) => Vector(vector).normalized;
        public static Vector3 Direction(Vector3 vector) => Vector(vector).normalized;
        public static Vector3 Direction(Vector3 to, Vector3 from) => Vector(to, from).normalized;
    }
}