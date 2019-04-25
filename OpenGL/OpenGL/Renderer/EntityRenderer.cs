using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace OpenGL
{
    public class EntityRenderer : BasicRenderer
    {
     
        public EntityRenderer(List<Entity> entities, EntityShader shader, EngineCamera engineCamera, List<DirectionalLight> directionalLights, List<PointLight> pointLights, List<SpotLight> spotLights, List<Material> materials)
        {
            this.Entities = new List<Entity>();
            this.Entities.AddRange(entities);
            this.Shader = shader;
            this.EngineCamera = engineCamera;
            this.DirectionalLights = directionalLights;
            this.PointLights = pointLights;
            this.SpotLights = spotLights;
            this.Materials = materials;
            EnableCulling();
        }
        EntityShader Shader;
        List<Entity> Entities;
        EngineCamera EngineCamera;
        List<DirectionalLight> DirectionalLights;
        List<PointLight> PointLights;
        List<SpotLight> SpotLights;
        List<Material> Materials;

        /// <summary>
        /// for optimization to render entites having same texture and vao
        /// </summary>
        private void RenderEntities()
        {
            EnableCulling(); 
            //todo handle culling according to transparency
            //todo handle using fake light
            //todo handle loading sky 
            //todo handle fog
            foreach (Entity entity in Entities)
            {
                BindModel(entity.RawModel);

                Shader.LoadModelMatrix(entity.Transformations.GetTransformation());
                Shader.LoadViewMatrix(EngineCamera.GetView());
                Shader.LoadProjectionMatrix(EngineCamera.GetProjection());
                Shader.LoadDirectionalLights(DirectionalLights);
                Shader.LoadPointLights(PointLights);
                Shader.LoadSpotLights(SpotLights);
                Shader.LoadMaterial(Materials);
                GL.DrawElements(BeginMode.Triangles, entity.RawModel.DrawNumber, DrawElementsType.UnsignedInt, 0);

                UnbindMaterial();
                UnbindModel();
            }
        }
        public override void Render()
        {
            Shader.Start();
            RenderEntities();
            Shader.Stop();
        }
        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }
        private void EnableCulling()
        {
            //do not render faces that are away of cam
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.DepthTest);
        }
        /// <summary>
        /// this method should be use if having transparent image
        /// </summary>
        private void DisableCulling()
        {
            //do consider rendering all parts 
            GL.Disable(EnableCap.CullFace);
        }
        public override void Clean()
        {
            Shader.CleanUp();
        }
        private static void UnbindModel()
        {
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);

            GL.BindVertexArray(0);
        }
        private void BindModel(RawModel rawModel)
        {
            GL.BindVertexArray(rawModel.VAO);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
        }
        private void UnbindMaterial()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
