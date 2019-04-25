using OpenGL.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenGL
{
    public class OBJIndex
    {
        public int VertexIndex { get; set; }
        public int TextureCoordIndex { get; set; }
        public int NormalIndex { get; set; }
    }
    /// <summary>
    /// for triangulated faces only made with 3 vertices 
    /// make sure normlas and uvs are included in file
    /// </summary>
    public class OBJModel
    {
        public OBJModel(string path)
        {
            Positions = new List<Vector3>();
            TextureCoords = new List<Vector2>();
            Normals = new List<Vector3>();
            Indices = new List<OBJIndex>();

            hasNormals = false;
            hasTexCoords = false;

            try
            {
                string[] lines;
                var list = new List<string>();
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {

                        var tokens = line.Split(' ');
                        if (line.StartsWith("v "))
                        {
                            Positions.Add(new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])));
                        }
                        else if (line.StartsWith("vn "))
                        {
                            Normals.Add(new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])));
                        }
                        else if (line.StartsWith("vt "))
                        {
                            TextureCoords.Add(new Vector2(float.Parse(tokens[1]), float.Parse(tokens[2])));
                        }
                        else if (line.StartsWith("f "))
                        {
                            for (int i = 0; i < tokens.Length - 3; i++)
                            {
                                Indices.Add(parseOBJIndex(tokens[1]));
                                Indices.Add(parseOBJIndex(tokens[2 + i]));
                                Indices.Add(parseOBJIndex(tokens[3 + i]));
                            }
                        }

                        list.Add(line);
                    }
                }
                lines = list.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Vector3> Positions { get; private set; }
        public List<Vector2> TextureCoords { get; private set; }
        public List<Vector3> Normals { get; private set; }
        public List<OBJIndex> Indices { get; private set; }
        private bool hasTexCoords;
        private bool hasNormals;

     

        /// <summary>
        /// takes string like 1/2/3
        /// </summary>
        /// <param name="s">token</param>
        /// <returns></returns>
        private OBJIndex parseOBJIndex(string s)
        {
            //c# is zero based i need to subract 1 
            OBJIndex oBJIndex = new OBJIndex();

            var indices = s.Split('/');
            oBJIndex.VertexIndex = int.Parse(indices[0]) - 1;
            if (indices.Length > 1)
            {
                hasTexCoords = true;
                oBJIndex.TextureCoordIndex = int.Parse(indices[1]) - 1;
                if (indices.Length > 2)
                {
                    hasNormals = true;
                    oBJIndex.NormalIndex = int.Parse(indices[2]) - 1;
                }
            }

            return oBJIndex;
        }


        public IndexedModel ToIndexedModel()
        {
            IndexedModel result = new IndexedModel();
            for (int i = 0; i < Indices.Count; i++)
            {
                OBJIndex currnetIndex = Indices[i];
                Vector3 currentPosition = Positions[currnetIndex.VertexIndex];
                Vector2 currentTextureCoord;
                Vector3 currentNormal;
                if (hasTexCoords)
                    currentTextureCoord = TextureCoords[currnetIndex.TextureCoordIndex];
                else
                    currentTextureCoord = new Vector2(0, 0);

                if (hasNormals)
                    currentNormal = Normals[currnetIndex.NormalIndex];
                else
                    currentNormal = new Vector3(0, 0, 0);

                result.Positions.Add(currentPosition);
                result.TextureCoords.Add(currentTextureCoord);
                result.Normals.Add(currentNormal);
                result.Indices.Add(i);

            }

            result.PostionsArray = BufferUtils.createFloatBuffer(result.Positions);
            result.NormalsArray = BufferUtils.createFloatBuffer(result.Normals);
            result.TextureCoordsArray = BufferUtils.createFloatBuffer(result.TextureCoords);
            result.IndicesArray = result.Indices.ToArray();

            return result;
        }

 
    }
}
