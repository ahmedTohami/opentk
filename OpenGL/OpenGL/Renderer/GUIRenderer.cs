using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL.Entities;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace OpenGL
{
    //public class GUIRenderer : BasicRenderer
    //{
    //    protected GUIRenderer()
    //    {
    //        float[] positions = { -1, 1, -1, -1, 1, 1, 1, -1 };
    //        Quad = Loader.getInstance().loadToVAO(positions,2);
    //        Shader = GUIShader.GetInstance();
    //    }
    //    private RawModel Quad;
    //    private static GUIRenderer Renderer;
    //    private GUIShader Shader;
    //    public static GUIRenderer GetInstance()
    //    {
    //        if (Renderer == null) Renderer = new GUIRenderer();
    //        return Renderer;
    //    }
    //    public void Render(List<GUITexture> textures)
    //    {
    //        Shader.Start();
    //        GL.BindVertexArray(Quad.VAO);
    //        GL.EnableVertexAttribArray(0);
    //        GL.Enable(EnableCap.Blend);
    //        GL.BlendFunc( BlendingFactor.SrcAlpha , BlendingFactor.OneMinusSrcAlpha);
    //        GL.Disable(EnableCap.DepthTest);
    //        foreach (GUITexture texture  in textures)
    //        {
    //            GL.ActiveTexture(TextureUnit.Texture0);
    //            GL.BindTexture(TextureTarget.Texture2D, texture.Id);
    //            Shader.loadTransformation(TransformationHelper.GetTransformation(texture.Position, texture.Scale));
    //            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, Quad.DrawNumber);
    //        }
    //        GL.Enable( EnableCap.DepthTest);
    //        GL.Disable(EnableCap.Blend);
    //        GL.DisableVertexAttribArray(0);
    //        GL.BindVertexArray(0);
    //        Shader.Stop();
    //    }
    //    public void Clean()
    //    {
    //        Shader.CleanUp();
    //    }
    //}
}
