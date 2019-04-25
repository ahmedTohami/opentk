using OpenTK;
namespace OpenGL
{
    public class Transformation
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public Transformation(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }
        public Matrix4 GetTransformation()
        {
            Matrix4 t = Matrix4.CreateTranslation(Position);

            Matrix4 r = Matrix4.CreateRotationX(Rotation.X)
                * Matrix4.CreateRotationY(Rotation.Y)
                * Matrix4.CreateRotationZ(Rotation.Z);

            Matrix4 s = Matrix4.CreateScale(Scale);

            //order from http://neokabuto.blogspot.com/2013/11/opentk-tutorial-4-its-amazing-way-you.html

            return s * r * t;
        }
    }
    public static class TransformationHelper
    {
        /// <summary>
        /// mainly for gui transformations
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix4 GetTransformation(Vector2 translation, Vector2 scale)
        {
            Matrix4 matrix = Matrix4.Identity;
            Matrix4 t = Matrix4.CreateTranslation(new Vector3(translation.X, translation.Y, 0));
            Matrix4 s = Matrix4.CreateScale(new Vector3(scale.X, scale.Y, 1f));
            return s * t;
        }
    }
}
