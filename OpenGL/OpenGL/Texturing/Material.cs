namespace OpenGL
{
    public class Material
    {
        public Material(int diffuse, int specular, float shininess)
        {
            Specular = specular;
            Diffuse = diffuse;
            Shininess = shininess;
        }
        public int Specular { get; set; }
        public int Diffuse { get; set; }
        public float Shininess { get; set; }
    }
}
