using OpenTK;
using System.Collections.Generic;

namespace OpenGL
{
    public class GUIShader : Shader
    {
        protected GUIShader() : base(new List<ShaderInfo>
        {
            new ShaderInfo("Shaders/GUIShader/guiVertexShader.glsl", OpenTK.Graphics.OpenGL4.ShaderType.VertexShader),
            new ShaderInfo("Shaders/GUIShader/guiFragmentShader.glsl", OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader)
        })
        { }
        private static GUIShader Shader;
        public static GUIShader GetInstance()
        {
            if (Shader == null) Shader = new GUIShader();
            return Shader;
        }
        public void LoadModel(Matrix4 model)
        {
            SetMatrix4(GetUniformLocation("model"), model);
        }
    }
}
