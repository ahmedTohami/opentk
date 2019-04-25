using OpenTK.Graphics.OpenGL4;
using System.Drawing.Imaging;
using System.Drawing;
namespace OpenGL
{
    public class Loader
    {
        private Loader()
        {

        }
        private static Loader loader;
        public static Loader GetInstance()
        {
            if (loader == null) loader = new Loader();
            return loader;
        }
        public RawModel LoadToVAO(float[] vertices, float[] uvs, float[] normals, int[] indices)
        {
            int vao = CreateVAO();
            BindIBO(indices);
            StoreDataInAttributeList(0, 3, vertices);
            StoreDataInAttributeList(1, 2, uvs);
            StoreDataInAttributeList(2, 3, normals);
            UnbindVAO();
            return new RawModel(vao, indices.Length);
        }
        /// <summary> 
        /// mainly for gui quads with dimension = 2 and cube maps with dimension= 3
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public RawModel LoadToVAO(float[] vertices, int dimension)
        {
            int vao = CreateVAO();
            StoreDataInAttributeList(0, dimension, vertices);
            UnbindVAO();
            return new RawModel(vao, vertices.Length / dimension);
        }
        public RawModel LoadModel(string path)
        {
            Loader Loader = Loader.GetInstance();
            OBJModel Box = new OBJModel(path);
            IndexedModel BoxIndexedModel = Box.ToIndexedModel();
            return Loader.LoadToVAO(BoxIndexedModel.PostionsArray, BoxIndexedModel.TextureCoordsArray, BoxIndexedModel.NormalsArray, BoxIndexedModel.IndicesArray);
        }
        private void StoreDataInAttributeList(int attribLocation, int size, float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            //feeding and fetching data
            GL.EnableVertexAttribArray(attribLocation);
            GL.VertexAttribPointer(attribLocation, size, VertexAttribPointerType.Float, false, 0, 0);
            //unbind
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        private void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }
        private void BindIBO(int[] indices)
        {
            int ibo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
        }
        private int CreateVAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            return vao;
        }
        public Texture LoadTexture(string path)
        {
            Bitmap bitmap = new Bitmap(path);

            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);

            GL.BindTexture(TextureTarget.Texture2D, tex);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, -0.4f);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return new Texture(tex);
        }
        public int LoadTextureID(string path)
        {
            Bitmap bitmap = new Bitmap(path);

            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, -0.4f);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return tex;
        }
        //public ModelTexture LoadModelTexture(string path)
        //{
        //    Bitmap bitmap = new Bitmap(path);

        //    int tex;
        //    GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

        //    GL.GenTextures(1, out tex);
        //    GL.BindTexture(TextureTarget.Texture2D, tex);

        //    BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
        //        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        //    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
        //        OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
        //    bitmap.UnlockBits(data);

        //    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, -0.4f);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        //    return new ModelTexture(new Texture(tex));
        //}
        public int LoadCubemap(string[] fileNames)
        {
            //todo change path in skybox class and here
            int texID = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texID);
            for (int i = 0; i < fileNames.Length; i++)
            {
                //decoding 
                Bitmap bitmap = new Bitmap("Res/" + fileNames[i]);

                BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                //+x , -x ,  +y ,-y ,+z , -z    
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0,
                    PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);
            }
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            return texID;
        }
    }
}
