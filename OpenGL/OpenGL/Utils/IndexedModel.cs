using System.Collections.Generic;
using OpenTK;

namespace OpenGL
{
    public class IndexedModel
    {
        public IndexedModel()
        {
            this.Positions = new List<Vector3>();
            this.Normals = new List<Vector3>();
            this.TextureCoords = new List<Vector2>();
            this.Indices = new List<int>();
        }
        public List<Vector3> Positions { get; set; }
        public List<Vector3> Normals { get; set; }
        public List<Vector2> TextureCoords { get; set; }
        public List<int> Indices { get; set; }

        public float[] PostionsArray { get; set; }
        public float[] NormalsArray { get; set; }
        public float[] TextureCoordsArray { get; set; }
        public int[] IndicesArray { get; set; }
    }
}
