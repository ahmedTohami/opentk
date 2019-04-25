using OpenTK;
namespace OpenGL
{
    public abstract class EngineCamera
    {
        public virtual Matrix4 GetView()
        {
            return Matrix4.Identity;
        }
        public virtual Matrix4 GetProjection()
        {
            return Matrix4.Identity;
        }
    }
}
