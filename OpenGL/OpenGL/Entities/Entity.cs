using OpenTK;
namespace OpenGL
{
    /// <summary>
    /// idea of entity represents 2 things 
    /// transformation and vao which holds data for this entity
    /// </summary>
    public class Entity
    {
        public RawModel RawModel { get; private set; }
        public Transformation Transformations { get; private set; }
        public int TextureIndex { get; set; }
        public Entity(Vector3 position, Vector3 rotation, Vector3 scale, RawModel rawModel)
        {
            Transformations = new Transformation(position, rotation, scale);
            this.RawModel = rawModel;
            TextureIndex = 0;
        }
        //public Entity(Vector3 position, Vector3 rotation, Vector3 scale, RawModel rawModel, ModelTexture ModelTexture, int textureIndex)
        //{
        //    Transformations = new Transformation(position, rotation, scale);
        //    this.RawModel = rawModel;
        //    this.TextureIndex = textureIndex;
        //    this.ModelTexture = ModelTexture;
        //}
        //public float GetTextureXOffset()
        //{
        //    int column = TextureIndex % ModelTexture.NumberOfRows;
        //    return (float)column / (float)ModelTexture.NumberOfRows;
        //}
        //public float GetTextureYOffset()
        //{
        //    int row = TextureIndex / ModelTexture.NumberOfRows;
        //    return (float)row / (float)ModelTexture.NumberOfRows;
        //}
        public void RotateX(float rot)
        {
            Transformations.Rotation = new Vector3(Transformations.Rotation.X + rot, Transformations.Rotation.Y, Transformations.Rotation.Z);
        }
        public void RotateY(float rot)
        {
            Transformations.Rotation = new Vector3(Transformations.Rotation.X, Transformations.Rotation.Y + rot, Transformations.Rotation.Z);
        }
        public void RotateZ(float rot)
        {
            Transformations.Rotation = new Vector3(Transformations.Rotation.X, Transformations.Rotation.Y, Transformations.Rotation.Z + rot);
        }
        public void TranslateX(float dt)
        {
            Transformations.Position = new Vector3(Transformations.Position.X + dt, Transformations.Position.Y, Transformations.Position.Z);
        }
        public void TranslateY(float dt)
        {
            Transformations.Position = new Vector3(Transformations.Position.X, Transformations.Position.Y + dt, Transformations.Position.Z);
        }
        public void TranslateZ(float dt)
        {
            Transformations.Position = new Vector3(Transformations.Position.X, Transformations.Position.Y, Transformations.Position.Z + dt);
        }
        public void Rotate(float rot)
        {
            RotateX(rot);
            RotateY(rot);
            RotateZ(rot);
        }
        public void Translate(float x, float y, float z)
        {
            TranslateX(x);
            TranslateY(y);
            TranslateZ(z);
        }
        public void Rotate(float rotx, float roty, float rotz)
        {
            RotateX(rotx);
            RotateY(roty);
            RotateZ(rotz);
        }
        public void Move(float move)
        {
            TranslateX(move);
            TranslateY(move);
            TranslateZ(move);
        }
    }
}
