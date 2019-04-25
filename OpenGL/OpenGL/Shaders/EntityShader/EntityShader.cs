using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;
namespace OpenGL
{
    public class EntityShader : Shader
    {
        protected EntityShader() : base(new List<ShaderInfo> {
            new ShaderInfo ("Shaders/EntityShader/BasicVS.glsl" , ShaderType.VertexShader),
            new ShaderInfo ("Shaders/EntityShader/BasicFS.glsl" , ShaderType.FragmentShader),
        })
        { }
        private int MAX_LIGHTS = 4;
        private int MAX_TEXTURES = 20;
        private static EntityShader Shader;
        public static EntityShader GetInstance()
        {
            if (Shader == null)
            {
                Shader = new EntityShader();
            }
            return Shader;
        }
        public void LoadDirectionalLights(List<DirectionalLight> directionalLights)
        {
            if(directionalLights != null && directionalLights.Count > 0)
            {
                if (directionalLights.Count > MAX_LIGHTS)
                {
                    System.Console.WriteLine("number of direcitonal lights exceeded 4 ");
                    return;
                }
                var Properties = typeof(DirectionalLight).GetProperties().Select(property => new { Name = property.Name, Type = property.PropertyType }).ToList();

                for (int i = 0; i < directionalLights.Count; i++)
                {
                    foreach (var property in Properties)
                    {
                        SetVector3(GetUniformLocation(string.Format("DirectionalLights[{0}].{1}", i, property.Name)), (Vector3)directionalLights[i].GetType().GetProperty(property.Name).GetValue(directionalLights[i], null));
                    }
                }

                //todo handle empty places in DirectionalLights array
            }
        }
        public void LoadSpotLights(List<SpotLight> spotLights)
        {
            if (spotLights != null && spotLights.Count > 0)
            {
                if (spotLights.Count > MAX_LIGHTS)
                {
                    System.Console.WriteLine("number of spot lights exceeded 4 ");
                    return;
                }

                var Properties = typeof(SpotLight).GetProperties().Select(property => new { Name = property.Name, Type = property.PropertyType }).ToList();
                for (int i = 0; i < spotLights.Count; i++)
                {
                    foreach (var property in Properties)
                    {
                        if (property.Type == typeof(float)) SetFloat(GetUniformLocation(string.Format("SpotLights[{0}].{1}", i, property.Name)), (float)spotLights[i].GetType().GetProperty(property.Name).GetValue(spotLights[i], null));
                        if (property.Type == typeof(Vector3)) SetVector3(GetUniformLocation(string.Format("SpotLights[{0}].{1}", i, property.Name)), (Vector3)spotLights[i].GetType().GetProperty(property.Name).GetValue(spotLights[i], null));
                    }
                }
                //todo handle empty places in SpotLights array
            }

        }
        public void LoadPointLights(List<PointLight> pointLights)
        {
            if (pointLights != null && pointLights.Count > 0)
            {
                if (pointLights.Count > MAX_LIGHTS)
                {
                    System.Console.WriteLine("number of point-lights exceeded 4 ");
                    return;
                }


                var Properties = typeof(PointLight).GetProperties().Select(property => new { Name = property.Name, Type = property.PropertyType }).ToList();
                for (int i = 0; i < pointLights.Count; i++)
                {
                    foreach (var property in Properties)
                    {
                        if (property.Type == typeof(float)) SetFloat(GetUniformLocation(string.Format("PointLights[{0}].{1}", i, property.Name)), (float)pointLights[i].GetType().GetProperty(property.Name).GetValue(pointLights[i], null));
                        if (property.Type == typeof(Vector3)) SetVector3(GetUniformLocation(string.Format("PointLights[{0}].{1}", i, property.Name)), (Vector3)pointLights[i].GetType().GetProperty(property.Name).GetValue(pointLights[i], null));
                    }
                }

                //todo handle empty places in PointLights array
            }


        }
        public void LoadMaterial(List<Material> materials)
        {
            if(materials != null && materials.Count > 0)
            {
                if (materials.Count > MAX_LIGHTS)
                {
                    System.Console.WriteLine("number of materials exceeded 20 ");
                    return;
                }

                Dictionary<string, int> materialsUnits = new Dictionary<string, int>();
                var Properties = typeof(Material).GetProperties().Select(property => new { Name = property.Name, Type = property.PropertyType }).ToList();

                int unit = 0;
                for (int i = 0; i < materials.Count; i++)
                {
                    foreach (var property in Properties)
                    {
                        if (property.Type == typeof(int))
                        {
                            var name = string.Format("Materials[{0}].{1}", i, property.Name);
                            SetInt(GetUniformLocation(name), unit);
                            materialsUnits.Add(name, unit);
                            unit++;
                        }
                        if (property.Type == typeof(float)) SetFloat(GetUniformLocation(string.Format("Materials[{0}].{1}", i, property.Name)), (float)materials[i].GetType().GetProperty(property.Name).GetValue(materials[i], null));
                    }
                }
                for (int i = 0; i < materialsUnits.Count; i++)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + materialsUnits[string.Format("Materials[{0}].Diffuse", i)]);
                    GL.BindTexture(TextureTarget.Texture2D, materials[i].Diffuse);
                    GL.ActiveTexture(TextureUnit.Texture0 + materialsUnits[string.Format("Materials[{0}].Specular", i)]);
                    GL.BindTexture(TextureTarget.Texture2D, materials[i].Specular);
                }

                //todo handle empty places in materials array
            }
        }
        public void LoadModelMatrix(Matrix4 model)
        {
            SetMatrix4(GetUniformLocation("model"), model);
        }
        public void LoadViewMatrix(Matrix4 view)
        {
            SetMatrix4(GetUniformLocation("view"), view);
        }
        public void LoadProjectionMatrix(Matrix4 projection)
        {
            SetMatrix4(GetUniformLocation("projection"), projection);
        }
    }
}
