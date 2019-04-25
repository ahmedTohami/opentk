using System;
using System.Collections.Generic;
using OpenGL.Entities;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace OpenGL
{
    public class MainGameLoop : GameWindow
    {
        private static MainGameLoop game;
        public static MainGameLoop GetInstance()
        {
            if (game == null) game = new MainGameLoop();
            return game;
        }
        protected MainGameLoop() : base(800, 800, new OpenTK.Graphics.GraphicsMode(), "Testing Window", GameWindowFlags.Default, DisplayDevice.Default, 3, 3, OpenTK.Graphics.GraphicsContextFlags.Default)
        { }


        EntityRenderer EntityRenderer = null;
        ModernCamera ModernCamera = null;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Clear();
            EntityRenderer.Render();
            SwapBuffers();
        }
        protected override void OnLoad(EventArgs e)
        {
            ModernCamera = ModernCamera.GetInstance();
            GenerateScene();
        }
        private void GenerateScene()
        {
            var loader = Loader.GetInstance();
            RawModel BoxRawModel = loader.LoadModel("Res/Models/woodenBox/woodenBox.obj");
            List<DirectionalLight> directionalLights = new List<DirectionalLight>();
            List<PointLight> pointLights = new List<PointLight>();
            List<SpotLight> spotLights = new List<SpotLight>();
            List<Entity> entities = new List<Entity>();
            List<Material> materials = new List<Material>();
            Material wood = new Material(loader.LoadTextureID("Res/Models/woodenBox/diffuse.png"), loader.LoadTextureID("Res/Models/woodenBox/specular.png"), 31);
            materials.Add(wood);

            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                entities.Add(new Entity(new Vector3(random.Next(-100, 100), 0, 3 * i), Vector3.Zero, Vector3.One, BoxRawModel));
            }

            directionalLights.Add(new DirectionalLight(Vector3.One, new Vector3(0.5f), new Vector3(0.3f), Vector3.One));
            directionalLights.Add(new DirectionalLight(Vector3.UnitX, new Vector3(0.3f), new Vector3(0.2f), Vector3.One));
            directionalLights.Add(new DirectionalLight(Vector3.UnitY, new Vector3(0.1f), new Vector3(0.2f), Vector3.One));
            directionalLights.Add(new DirectionalLight(Vector3.UnitZ, new Vector3(0.8f), new Vector3(0.1f), Vector3.One));

            pointLights.Add(new PointLight(new Vector3(0, 10, 0), new Vector3(0.3f), new Vector3(0.3f), Vector3.One * 5, 1, 0.05f, 0.003f));
            pointLights.Add(new PointLight(new Vector3(10, 10, 10), new Vector3(0.4f), new Vector3(0.5f), Vector3.One * 5, 1, 0.05f, 0.003f));
            pointLights.Add(new PointLight(new Vector3(15, 10, 10), new Vector3(0.5f), new Vector3(0.2f), Vector3.One * 5, 1, 0.05f, 0.003f));
            pointLights.Add(new PointLight(new Vector3(15, 10, 15), new Vector3(0.8f), new Vector3(0.1f), Vector3.One * 5, 1, 0.05f, 0.003f));

            spotLights.Add(new SpotLight(new Vector3(0, 15, 0), Vector3.One, new Vector3(0.3f), new Vector3(0.5f), Vector3.One * 10, 1, 0.05f, 0.003f, (float)Math.Cos(MathHelper.DegreesToRadians(15)), (float)Math.Cos(MathHelper.DegreesToRadians(17))));
            spotLights.Add(new SpotLight(new Vector3(5, 15, 5), Vector3.One, new Vector3(0.3f), new Vector3(0.5f), Vector3.One * 10, 1, 0.05f, 0.003f, (float)Math.Cos(MathHelper.DegreesToRadians(15)), (float)Math.Cos(MathHelper.DegreesToRadians(17))));
            spotLights.Add(new SpotLight(new Vector3(10, 15, 10), Vector3.One, new Vector3(0.3f), new Vector3(0.5f), Vector3.One * 10, 1, 0.05f, 0.003f, (float)Math.Cos(MathHelper.DegreesToRadians(15)), (float)Math.Cos(MathHelper.DegreesToRadians(17))));
            spotLights.Add(new SpotLight(new Vector3(15, 15, 15), Vector3.One, new Vector3(0.3f), new Vector3(0.5f), Vector3.One * 10, 1, 0.05f, 0.003f, (float)Math.Cos(MathHelper.DegreesToRadians(15)), (float)Math.Cos(MathHelper.DegreesToRadians(17))));

            EntityRenderer = new EntityRenderer(entities, EntityShader.GetInstance(), ModernCamera, directionalLights, pointLights, spotLights, materials);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard.GetState().IsKeyDown(Key.W)) ModernCamera.ProcessKeyboard(CAMERA_MOVE_DIRECTION.FORWARD, (float)e.Time);
            if (Keyboard.GetState().IsKeyDown(Key.S)) ModernCamera.ProcessKeyboard(CAMERA_MOVE_DIRECTION.BACKWARD, (float)e.Time);
            if (Keyboard.GetState().IsKeyDown(Key.A)) ModernCamera.ProcessKeyboard(CAMERA_MOVE_DIRECTION.LEFT, (float)e.Time);
            if (Keyboard.GetState().IsKeyDown(Key.D)) ModernCamera.ProcessKeyboard(CAMERA_MOVE_DIRECTION.RIGHT, (float)e.Time);
            if (Keyboard.GetState().IsKeyDown(Key.Up)) ModernCamera.ProcessKeyboard(CAMERA_MOVE_DIRECTION.UP, (float)e.Time);
            if (Keyboard.GetState().IsKeyDown(Key.Down)) ModernCamera.ProcessKeyboard(CAMERA_MOVE_DIRECTION.DOWN, (float)e.Time);

        }



        public override void Exit()
        {
            EntityRenderer.Clean();
        }
        private static void Clear()
        {
            GL.ClearColor(Constants.SkyRed, Constants.SkyGreen, Constants.SkyBlue, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            ModernCamera.ProcessMouseMovement(e.XDelta, e.YDelta);
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            ModernCamera.ProcessMouseScroll(e.DeltaPrecise);
        }
    }
}
