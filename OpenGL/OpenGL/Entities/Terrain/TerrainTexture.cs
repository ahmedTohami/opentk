namespace OpenGL
{
    public class TerrainTexture
    {
        public Texture  Texture{ get; private set; }
        public TerrainTexture(Texture texture)
        {
            this.Texture = texture;
        }
    }
}
