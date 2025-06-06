using UnityEngine;
using System;
using Pathfinding;

namespace HoneyFramework
{
    [Serializable]
    public struct Vector3i
    {

        public int x, y, z;

        public static readonly Vector3i zero = new Vector3i(0, 0, 0);
        public static readonly Vector3i one = new Vector3i(1, 1, 1);
        public static readonly Vector3i forward = new Vector3i(0, 0, 1);
        public static readonly Vector3i back = new Vector3i(0, 0, -1);
        public static readonly Vector3i up = new Vector3i(0, 1, 0);
        public static readonly Vector3i down = new Vector3i(0, -1, 0);
        public static readonly Vector3i left = new Vector3i(-1, 0, 0);
        public static readonly Vector3i right = new Vector3i(1, 0, 0);

        public static readonly Vector3i[] directions = new Vector3i[] {
		left, right,
		back, forward,
		down, up,
	};

        public Vector3i(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3i(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }

        #region Pathfinder A*
        /// <summary>
        /// Special conversion from A* pathfinding library Int3 to Vector3. Note that Int3 uses multiplication by 1000 of all values before changing float to integer.
        /// It maybe dangerous to use them directly as you may endup easily far away from your world size
        /// </summary>
        /// <param name="pathfinderPosition"></param>
        /// <returns></returns>
        public Vector3i(Int3 pathfinderPosition)
        {
            Vector3 v = (Vector3)pathfinderPosition;
            x = Mathf.RoundToInt(v.x);
            y = Mathf.RoundToInt(v.y);
            z = Mathf.RoundToInt(v.z);
        }

        public Int3 ToInt3()
        {
            return new Int3(new Vector3(x, y, z));
        }
        #endregion

        public static int DistanceSquared(Vector3i a, Vector3i b)
        {
            int dx = b.x - a.x;
            int dy = b.y - a.y;
            int dz = b.z - a.z;
            return dx * dx + dy * dy + dz * dz;
        }

        public int DistanceSquared(Vector3i v)
        {
            return DistanceSquared(this, v);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3i)) return false;
            Vector3i vector = (Vector3i)other;
            return x == vector.x &&
                   y == vector.y &&
                   z == vector.z;
        }

        public override string ToString()
        {
            return "Vector3i(" + x + " " + y + " " + z + ")";
        }        

        public static Vector3i Min(Vector3i a, Vector3i b)
        {
            return new Vector3i(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
        }
        public static Vector3i Max(Vector3i a, Vector3i b)
        {
            return new Vector3i(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
        }

        public static bool operator ==(Vector3i a, Vector3i b)
        {
            return a.x == b.x &&
                   a.y == b.y &&
                   a.z == b.z;
        }

        public static bool operator !=(Vector3i a, Vector3i b)
        {
            return a.x != b.x ||
                   a.y != b.y ||
                   a.z != b.z;
        }

        public static Vector3i operator -(Vector3i a, Vector3i b)
        {
            return new Vector3i(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3i operator +(Vector3i a, Vector3i b)
        {
            return new Vector3i(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static implicit operator Vector3(Vector3i v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        static public Vector3i Convert(Vector3 floatVector)
        {
            return Convert(floatVector.x, floatVector.y, floatVector.z);
        }

        static public Vector3i Convert(float x, float y, float z)
        {

            int ix = (int)(x > 0 ? x + 0.5f : x - 0.5f);
            int iy = (int)(y > 0 ? y + 0.5f : y - 0.5f);
            int iz = (int)(z > 0 ? z + 0.5f : z - 0.5f);

            return new Vector3i(ix, iy, iz);
        }
    }
}