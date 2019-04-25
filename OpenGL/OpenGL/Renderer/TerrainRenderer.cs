using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System.Collections.Generic;

namespace OpenGL
{
    //public class TerrainRenderer : BasicRenderer
    //{
    //    TerrainShader Shader;
    //    List<Terrain> Terrains;
       


    //    /// <summary>
    //    /// for optimization to render entites having same texture and vao
    //    /// </summary>
    //    public TerrainRenderer(List<Terrain> terrains)
    //    {
    //        this.Shader = TerrainShader.GetInstance();
    //        this.Terrains = new List<Terrain>();
    //        this.Terrains.AddRange(terrains);
    //        EnableCulling();

    //        this.Shader.Start();
    //        this.Shader.LoadUniformLocations();
    //        this.Shader.ConnectTextureUnits();
    //    }

    //    public void AddTerrain(Terrain terrain)
    //    {
    //        Terrains.Add(terrain);
    //    }
    //    private void UnbindModel()
    //    {
    //        GL.DisableVertexAttribArray(0);
    //        GL.DisableVertexAttribArray(1);
    //        GL.DisableVertexAttribArray(2);
    //        GL.BindVertexArray(0);
    //        EnableCulling();
    //    }

    //    private void BindModel(RawModel rawModel)
    //    {
    //        GL.BindVertexArray(rawModel.VAO);
    //        GL.EnableVertexAttribArray(0);
    //        GL.EnableVertexAttribArray(1);
    //        GL.EnableVertexAttribArray(2);
    //    }


    //    private void BindTextures(Terrain terrain)
    //    {
    //        GL.ActiveTexture(TextureUnit.Texture0);
    //        GL.BindTexture(TextureTarget.Texture2D, terrain.Pack.BackgroundTexture.Texture.Id);

    //        GL.ActiveTexture(TextureUnit.Texture1);
    //        GL.BindTexture(TextureTarget.Texture2D, terrain.Pack.RTexture.Texture.Id);

    //        GL.ActiveTexture(TextureUnit.Texture2);
    //        GL.BindTexture(TextureTarget.Texture2D, terrain.Pack.GTexture.Texture.Id);

    //        GL.ActiveTexture(TextureUnit.Texture3);
    //        GL.BindTexture(TextureTarget.Texture2D, terrain.Pack.BTexture.Texture.Id);

    //        GL.ActiveTexture(TextureUnit.Texture4);
    //        GL.BindTexture(TextureTarget.Texture2D, terrain.BlendMap.Texture.Id);
    //    }

    //    private void EnableCulling()
    //    {
    //        //do not render faces that are away of cam
    //        GL.Enable(EnableCap.CullFace);
    //        GL.CullFace(CullFaceMode.Back);
    //        GL.Enable(EnableCap.DepthTest);
    //    }

    //    public void Render()
    //    {

    //        Shader.Start();

    //        renderTerrains();

    //        Shader.Stop();
    //    }

    //    public void clean()
    //    {
    //        Shader.CleanUp();
    //    }

    //    /// <summary>
    //    /// for optimization to render entites having same texture and vao
    //    /// </summary>
    //    private void renderTerrains()
    //    {
    //        BindModel(Terrains[0].RawModel); //all terrains have same raw model
    //        foreach (Terrain terrain in Terrains)
    //        {
    //            BindTextures(terrain);

    //            ((TerrainShader)Shader).LoadShineVariables(0, 1);
    //            ((TerrainShader)Shader).LoadSkyColor(Constants.SkyRed, Constants.SkyGreen, Constants.SkyBlue);
    //            ((TerrainShader)Shader).loadLights(Lights);

    //            Shader.update(new ShaderUpdateDate { Model = terrain.Transformations.GetTransformation(), View = ThirdPersonCamera.getInstance().getView(), Projection = ThirdPersonCamera.getInstance().getProjection() });
    //            GL.DrawElements(BeginMode.Triangles, terrain.RawModel.DrawNumber, DrawElementsType.UnsignedInt, 0);
    //        }

    //        UnbindModel();

    //    }
    //}
}
