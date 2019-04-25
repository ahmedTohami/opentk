using OpenTK;
using System;
using System.Drawing;

namespace OpenGL
{
    public class Terrain
    {
        private int VERTEX_COUNT = 100;
        private int SIZE = 800;
        float[,] heights;
        public RawModel RawModel { get; private set; }
        public TerrainTextureBack Pack { get; private set; }
        public TerrainTexture BlendMap { get; private set; }
        public Transformation Transformations { get; private set; }
        private float x;
        private float z;

        public Terrain(int gridX, int gridZ, TerrainTexture blendMap, TerrainTextureBack pack, string heightMap)
        {
            Transformations = new Transformation(Vector3.Zero, Vector3.Zero, Vector3.One);
            this.Pack = pack;
            this.BlendMap = blendMap;
            x = gridX * SIZE;
            z = gridZ * SIZE;
            RawModel = GenerateTerrain(heightMap);
        }
        public float GetTerrainHeight(float worldX, float worldZ)
        {
            float terrainX = worldX - this.x;
            float terrainZ = worldZ - this.z;
            float gridSquareSize = SIZE / (float)(heights.GetLength(0) - 1);
            int gridX = (int)Math.Floor(terrainX / gridSquareSize);
            int gridZ = (int)Math.Floor(terrainZ / gridSquareSize);
            if (gridX >= heights.GetLength(0) - 1 || gridZ >= heights.GetLength(1) - 1 || gridX < 0 || gridZ < 0)
            {
                return 0;
            }
            float xCoord = (terrainX % gridSquareSize) / gridSquareSize;
            float zCoord = (terrainZ % gridSquareSize) / gridSquareSize;
            float answer;
            if (xCoord <= (1 - zCoord))
            {
                answer = BarryCentric(new Vector3(0, heights[gridX, gridZ], 0), new Vector3(1, heights[gridX + 1,gridZ], 0), new Vector3(0, heights[gridX,gridZ + 1], 1), new Vector2(xCoord, zCoord));
            }
            else
            {
                answer = BarryCentric(new Vector3(1, heights[gridX + 1,gridZ], 0), new Vector3(1, heights[gridX + 1,gridZ + 1], 1), new Vector3(0, heights[gridX,gridZ + 1], 1), new Vector2(xCoord, zCoord));
            }
            return answer;

        }
        private static float BarryCentric(Vector3 p1, Vector3 p2, Vector3 p3, Vector2 pos)
        {
            float det = (p2.Z - p3.Z) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Z - p3.Z);
            float l1 = ((p2.Z - p3.Z) * (pos.X - p3.X) + (p3.X - p2.X) * (pos.Y - p3.Z)) / det;
            float l2 = ((p3.Z - p1.Z) * (pos.X - p3.X) + (p1.X - p3.X) * (pos.Y - p3.Z)) / det;
            float l3 = 1.0f - l1 - l2;
            return l1 * p1.Y + l2 * p2.Y + l3 * p3.Y;
        }
        private RawModel GenerateTerrain(string heightMap)
        {
            string path = "Res/" + heightMap;
            Bitmap image = new Bitmap(path);


            VERTEX_COUNT = image.Height;
            heights = new float[VERTEX_COUNT, VERTEX_COUNT];

            int count = VERTEX_COUNT * VERTEX_COUNT;
            float[] vertices = new float[count * 3];
            float[] normals = new float[count * 3];
            float[] textureCoords = new float[count * 2];
            int[] indices = new int[6 * (VERTEX_COUNT - 1) * (VERTEX_COUNT - 1)];
            int vertexPointer = 0;
            for (int i = 0; i < VERTEX_COUNT; i++)
            {
                for (int j = 0; j < VERTEX_COUNT; j++)
                {
                    vertices[vertexPointer * 3] = (float)j / ((float)VERTEX_COUNT - 1) * SIZE;
                    var height = GetHeight(j, i, image);
                    heights[j, i] = height;
                    vertices[vertexPointer * 3 + 1] = height;
                    vertices[vertexPointer * 3 + 2] = (float)i / ((float)VERTEX_COUNT - 1) * SIZE;
                    Vector3 normal = CalculateNormal(j, i, image);
                    normals[vertexPointer * 3] = normal.X;
                    normals[vertexPointer * 3 + 1] = normal.Y;
                    normals[vertexPointer * 3 + 2] = normal.Z;
                    textureCoords[vertexPointer * 2] = (float)j / ((float)VERTEX_COUNT - 1);
                    textureCoords[vertexPointer * 2 + 1] = (float)i / ((float)VERTEX_COUNT - 1);
                    vertexPointer++;
                }
            }
            int pointer = 0;
            for (int gz = 0; gz < VERTEX_COUNT - 1; gz++)
            {
                for (int gx = 0; gx < VERTEX_COUNT - 1; gx++)
                {
                    int topLeft = (gz * VERTEX_COUNT) + gx;
                    int topRight = topLeft + 1;
                    int bottomLeft = ((gz + 1) * VERTEX_COUNT) + gx;
                    int bottomRight = bottomLeft + 1;
                    indices[pointer++] = topLeft;
                    indices[pointer++] = bottomLeft;
                    indices[pointer++] = topRight;
                    indices[pointer++] = topRight;
                    indices[pointer++] = bottomLeft;
                    indices[pointer++] = bottomRight;
                }
            }
            return Loader.GetInstance().LoadToVAO(vertices, textureCoords, normals, indices);
        }
        private float GetHeight(int x, int z, Bitmap image)
        {
            if (x < 0 || x >= image.Height || z < 0 || z >= image.Height) return 0;
            return image.GetPixel(x, z).R;
        }
        private Vector3 CalculateNormal(int x, int z, Bitmap image)
        {
            if (x < 1 || x >= image.Width - 1 || z < 1 || z >= image.Height - 1) return new Vector3(0, 1, 0);
            float L = image.GetPixel(x - 1, z).R;
            float R = image.GetPixel(x + 1, z).R;
            float D = image.GetPixel(x, z - 1).R;
            float U = image.GetPixel(x, z + 1).R;
            Vector3 normal = new Vector3(L - R, 2f, D - U);
            return normal.Normalized();
        }
    }
}
