using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Utils
{
    public static class BufferUtils
    {
        public static float[] createFloatBuffer(List<Vector3> data)
        {
            List<float> result = new List<float>();
            for (int i = 0; i < data.Count; i++)
            {
                result.Add(data[i].X);
                result.Add(data[i].Y);
                result.Add(data[i].Z);
            }
            return result.ToArray();
        }

        public static float[] createFloatBuffer(List<Vector2> data)
        {
            List<float> result = new List<float>();
            for (int i = 0; i < data.Count; i++)
            {
                result.Add(data[i].X);
                result.Add(data[i].Y);
            }
            return result.ToArray();
        }

    }
}
