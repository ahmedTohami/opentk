using OpenTK.Graphics.OpenGL4;
namespace OpenGL
{
    /// <summary>
    /// to use texture class create new texture
    /// texture.bind to bind active first texture
    /// texture.delete
    /// </summary>
    public class Texture
    {
        public int Id  { get;private set; }
        public Texture(int id)
        {
            this.Id = id;
        }
        public void Bind()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Id);
        }
        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
        public void Delete()
        {
            GL.DeleteTexture(Id);
        }
    }
}
