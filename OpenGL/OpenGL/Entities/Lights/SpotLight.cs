using OpenTK;
namespace OpenGL
{
    public class SpotLight
    {
        public SpotLight(Vector3 position, Vector3 direction, Vector3 ambient, Vector3 diffuse, Vector3 specular, float constant, float linear, float quadratic, float cutOff, float outerCutOff)
        {
            Position = position;
            Direction = direction;
            this.Ambient = ambient;
            this.Diffuse = diffuse;
            this.Specular = specular;
            this.Constant = constant;
            this.Linear = linear;
            this.Quadratic = quadratic;
            this.CutOff = cutOff;
            this.OuterCutOff = outerCutOff;
        }
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public Vector3 Ambient { get; set; }
        public Vector3 Diffuse { get; set; }
        public Vector3 Specular { get; set; }
        public float Constant { get; set; }
        public float Linear { get; set; }
        public float Quadratic { get; set; }
        public float CutOff { get; set; }
        public float OuterCutOff { get; set; }
    }
}
