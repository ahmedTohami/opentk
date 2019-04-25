using System;
using OpenTK;
using OpenTK.Input;
namespace OpenGL
{
    public enum CAMERA_MOVE_DIRECTION
    {
        FORWARD,
        BACKWARD,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    public class Camera:EngineCamera
    {
        protected Camera()
        {
            Console.WriteLine("Creating Camera Object");
        }

        private static Camera cam;
        public static Camera GetInstance()
        {
            if (cam == null) cam = new Camera();
            return cam;
        }

        private Vector3 Eye = new Vector3(100, 20, 150);
        private Vector3 Front = -Vector3.UnitZ;
        private Vector3 Up = Vector3.UnitY;


        public void Move(CAMERA_MOVE_DIRECTION dir)
        {
            if (dir == CAMERA_MOVE_DIRECTION.FORWARD) Eye.Z -= 1f * Constants.FASTSPEED;
            else if (dir == CAMERA_MOVE_DIRECTION.BACKWARD) Eye.Z += 1f * Constants.FASTSPEED;

            else if (dir == CAMERA_MOVE_DIRECTION.RIGHT) Eye.X += 1f * Constants.FASTSPEED;
            else if (dir == CAMERA_MOVE_DIRECTION.LEFT) Eye.X -= 1f * Constants.FASTSPEED;

            else if (dir == CAMERA_MOVE_DIRECTION.UP) Eye.Y += 1f * Constants.FASTSPEED;
            else if (dir == CAMERA_MOVE_DIRECTION.DOWN) Eye.Y -= 1f * Constants.FASTSPEED;


            if (Eye.Y < 1) Eye.Y = 1;
        }
        public void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Key.W)) Move(CAMERA_MOVE_DIRECTION.FORWARD);
            else if (Keyboard.GetState().IsKeyDown(Key.S)) Move(CAMERA_MOVE_DIRECTION.BACKWARD);
            else if (Keyboard.GetState().IsKeyDown(Key.A)) Move(CAMERA_MOVE_DIRECTION.LEFT);
            else if (Keyboard.GetState().IsKeyDown(Key.D)) Move(CAMERA_MOVE_DIRECTION.RIGHT);
            else if (Keyboard.GetState().IsKeyDown(Key.Q)) Move(CAMERA_MOVE_DIRECTION.UP);
            else if (Keyboard.GetState().IsKeyDown(Key.E)) Move(CAMERA_MOVE_DIRECTION.DOWN);
        }
        public override Matrix4 GetView()
        {
            return Matrix4.LookAt(Eye, Eye + Front, Up);
        }
        public override Matrix4 GetProjection()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Constants.FOV), Constants.ASPECT, Constants.ZNEAR, Constants.ZFAR);
        }
    }
}
