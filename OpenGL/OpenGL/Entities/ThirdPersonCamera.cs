using System;
using OpenTK;
using OpenTK.Input;

namespace OpenGL
{
    public class ThirdPersonCamera : EngineCamera
    {
        protected ThirdPersonCamera(Player player)
        {
            Console.WriteLine("Creating ThirdPersonCamera Object");
            this.Player = player;
        }

        private static ThirdPersonCamera cam;
        public static ThirdPersonCamera GetInstance(Player player)
        {
            if (cam == null) cam = new ThirdPersonCamera(player);
            return cam;
        }


        private Vector3 Position = Vector3.Zero;
        private float Pitch = 20;
        private float Yaw = 0;
        private float Roll = 0;


        private float DistanceFromPlayer = 100;
        private float AngleAroundPlayer = 0;

        public Player Player { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoom">wheel delta</param>
        private void CalculateZoom(float zoom)
        {
            DistanceFromPlayer -= zoom * Constants.ZOOM_SENSITIVITY;
            if (DistanceFromPlayer < 20) DistanceFromPlayer = 20;
        }
        private void CalculatePitch(float YDelta)
        {
            //mouse moved for how long in up and down
            if (Mouse.GetState().IsButtonDown(MouseButton.Right))
            {
                Pitch -= YDelta * Constants.ROTATION_SENSITIVITY;

                if (Pitch < 2) Pitch = 2;
                if (Pitch > 85) Pitch = 85;
            }
        }
        private void CalculateAngleAroundPlayer(float XDelta)
        {
            //mouse moved for how long in left and right
            if (Mouse.GetState().IsButtonDown(MouseButton.Left))
            {
                AngleAroundPlayer -= XDelta * Constants.ROTATION_SENSITIVITY;
            }
        }
        public void Move(float zoom, float XDelta, float YDelta)
        {
            CalculateZoom(zoom);
            CalculatePitch(YDelta);
            CalculateAngleAroundPlayer(XDelta);
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            float horizontalDistance = (float)(DistanceFromPlayer * Math.Cos(MathHelper.DegreesToRadians(Pitch)));
            float verticalDistance = (float)(DistanceFromPlayer * Math.Sin(MathHelper.DegreesToRadians(Pitch)));


            float theta = AngleAroundPlayer + Player.Transformations.Rotation.Y;
            float xOffset = horizontalDistance * (float)Math.Sin(MathHelper.DegreesToRadians(theta));
            float zOffset = horizontalDistance * (float)Math.Cos(MathHelper.DegreesToRadians(theta));

            Position.X = Player.Transformations.Position.X - xOffset;
            Position.Z = Player.Transformations.Position.Z - zOffset;
            Position.Y = Player.Transformations.Position.Y + verticalDistance;

            Yaw = 180 - (Player.Transformations.Rotation.Y + AngleAroundPlayer);
        }
        public override Matrix4 GetProjection()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Constants.FOV), Constants.ASPECT, Constants.ZNEAR, Constants.ZFAR);
        }

        public override Matrix4 GetView()
        {
            return Matrix4.LookAt(Position, Player.Transformations.Position, Vector3.UnitY);
        }
    }
}
