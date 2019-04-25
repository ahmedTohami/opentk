using OpenTK.Graphics.OpenGL4;

namespace OpenGL
{
    //public class SkyBoxRenderer : BasicRenderer
    //{

    //    string[] fileNames = new string[] { "right.png", "left.png", "top.png", "bottom.png", "back.png", "front.png" };
    //    public SkyBoxRenderer() 
    //    {
    //        cube = Loader.getInstance().loadToVAO(VERTICES, 3);
    //        Texture = Loader.getInstance().LoadCubemap(fileNames);
    //        Shader = new SkyboxShader();
    //    }
    //    private static float SIZE = 1000f;
    //    private static float[] VERTICES = {
    //    -SIZE,  SIZE, -SIZE,
    //    -SIZE, -SIZE, -SIZE,
    //     SIZE, -SIZE, -SIZE,
    //     SIZE, -SIZE, -SIZE,
    //     SIZE,  SIZE, -SIZE,
    //    -SIZE,  SIZE, -SIZE,

    //    -SIZE, -SIZE,  SIZE,
    //    -SIZE, -SIZE, -SIZE,
    //    -SIZE,  SIZE, -SIZE,
    //    -SIZE,  SIZE, -SIZE,
    //    -SIZE,  SIZE,  SIZE,
    //    -SIZE, -SIZE,  SIZE,

    //     SIZE, -SIZE, -SIZE,
    //     SIZE, -SIZE,  SIZE,
    //     SIZE,  SIZE,  SIZE,
    //     SIZE,  SIZE,  SIZE,
    //     SIZE,  SIZE, -SIZE,
    //     SIZE, -SIZE, -SIZE,

    //    -SIZE, -SIZE,  SIZE,
    //    -SIZE,  SIZE,  SIZE,
    //     SIZE,  SIZE,  SIZE,
    //     SIZE,  SIZE,  SIZE,
    //     SIZE, -SIZE,  SIZE,
    //    -SIZE, -SIZE,  SIZE,

    //    -SIZE,  SIZE, -SIZE,
    //     SIZE,  SIZE, -SIZE,
    //     SIZE,  SIZE,  SIZE,
    //     SIZE,  SIZE,  SIZE,
    //    -SIZE,  SIZE,  SIZE,
    //    -SIZE,  SIZE, -SIZE,

    //    -SIZE, -SIZE, -SIZE,
    //    -SIZE, -SIZE,  SIZE,
    //     SIZE, -SIZE, -SIZE,
    //     SIZE, -SIZE, -SIZE,
    //    -SIZE, -SIZE,  SIZE,
    //     SIZE, -SIZE,  SIZE
    //};

    //    public RawModel cube { get; private set; }
    //    public int Texture { get; private set; }
    //    public SkyboxShader Shader { get; private set; }
    //    public void render()
    //    {
    //        Shader.Start();
    //        Shader.LoadUniformLocations();
    //        Shader.update(new ShaderUpdateDate { View = ThirdPersonCamera.getInstance().getView(), Projection = ThirdPersonCamera.getInstance().getProjection() });
    //        GL.BindVertexArray(cube.VAO);
    //        GL.EnableVertexAttribArray(0);
    //        GL.ActiveTexture(TextureUnit.Texture0);
    //        GL.BindTexture(TextureTarget.TextureCubeMap, Texture);
    //        GL.DrawArrays(PrimitiveType.Triangles, 0, cube.DrawNumber);
    //        GL.DisableVertexAttribArray(0);
    //        GL.BindVertexArray(0);
    //        Shader.Stop();
    //    }

    //}
}
