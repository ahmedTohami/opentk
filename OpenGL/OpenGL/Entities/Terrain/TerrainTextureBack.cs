namespace OpenGL
{
    public class TerrainTextureBack
    {
        public TerrainTextureBack(TerrainTexture backgroundTexture, TerrainTexture rTexture, TerrainTexture gTexture, TerrainTexture bTexture)
        {
            this.BackgroundTexture = backgroundTexture;
            this.RTexture = rTexture;
            this.GTexture = gTexture;
            this.BTexture = bTexture;
        }
        public TerrainTexture BackgroundTexture { get; private set; }
        public TerrainTexture RTexture { get; private set; }
        public TerrainTexture GTexture { get; private set; }
        public TerrainTexture BTexture { get; private set; }
    }
}
