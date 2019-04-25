namespace OpenGL
{
    /// <summary>
    /// holds vao for a model and draw number
    /// </summary>
    public class RawModel
    {
        public int VAO { get;private set; }
        public int DrawNumber { get; private set; }
        public RawModel(int vao , int drawNumber)
        {
            this.VAO = vao;
            this.DrawNumber = drawNumber;
        }
    }
}
