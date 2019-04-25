using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace OpenGL
{
    public class SkyboxShader : Shader
    {
        protected SkyboxShader() : base(new List<ShaderInfo> {
            new ShaderInfo ("Shaders/SkyboxShader/skyboxVS.glsl",ShaderType.VertexShader),
            new ShaderInfo ("Shaders/SkyboxShader/skyboxFS.glsl",ShaderType.FragmentShader),
        })
        { }
        private SkyboxShader Shader;
        public SkyboxShader GetInstance()
        {
            if (Shader == null) Shader = new SkyboxShader();
            return Shader;
        }
        public void LoadViewMatrix(Matrix4 view)
        {
            view.M31 = 0;
            view.M32 = 0;
            view.M33 = 0;
            SetMatrix4(GetUniformLocation("view"), view);
        }
        public void LoadProjectionMatrix(Matrix4 projection)
        {
            SetMatrix4(GetUniformLocation("projection"), projection);
        }
    }
}
