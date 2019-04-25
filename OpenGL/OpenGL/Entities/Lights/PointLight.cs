using OpenTK;
namespace OpenGL
{
    public class PointLight
    {
        public PointLight(Vector3 position, Vector3 ambient, Vector3 diffuse, Vector3 specular, float constant, float linear, float quadratic)
        {
            Position = position;
            this.Ambient = ambient;
            this.Diffuse = diffuse;
            this.Specular = specular;
            this.Constant = constant;
            this.Linear = linear;
            this.Quadratic = quadratic;
        }
        public Vector3 Position { get; set; }
        public Vector3 Ambient { get; set; }
        public Vector3 Diffuse { get; set; }
        public Vector3 Specular { get; set; }
        public float Constant { get; set; }
        public float Linear { get; set; }
        public float Quadratic { get; set; }
    }
}
