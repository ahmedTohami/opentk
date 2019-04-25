using System;
using OpenTK;
namespace OpenGL
{
    public class ModernCamera : EngineCamera
    {
        const float PITCH = 0;
        const float YAW = -90f;
        const float SPEED = 6f;
        const float SENSITIVITY = 0.25f;
        const float ZOOM = 45f;

        public float Yaw { get; private set; }
        public float Pitch { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Front { get; private set; }
        public Vector3 Up { get; private set; }
        private Vector3 Right;
        public float MovementSpeed { get; private set; }
        public float MouseSensitivity { get; private set; }
        public float Zoom { get; private set; }
        private Vector3 WorldUp = Vector3.UnitY;


         
        private static ModernCamera cam;
        public static ModernCamera GetInstance()
        {
            if (cam == null) cam = new ModernCamera();
            return cam;
        }

        protected ModernCamera()
        {
            Console.WriteLine("Creating ModernCamera Object");
            this.Position = Vector3.Zero;
            this.Yaw = YAW;
            this.Pitch = PITCH;
            this.Front = Vector3.UnitZ * -1f;
            this.MouseSensitivity = SENSITIVITY;
            this.MovementSpeed = SPEED;
            this.UpdateCameraVectors();
            this.WorldUp = Vector3.UnitY;
            this.Up = Vector3.UnitY;
            this.Zoom = ZOOM;
        }

        private void UpdateCameraVectors()
        {
            Vector3 front = Vector3.Zero;
            front.X = (float)(Math.Cos(MathHelper.DegreesToRadians(this.Yaw)) * Math.Cos(MathHelper.DegreesToRadians(this.Pitch)));
            front.Y = (float)(Math.Sin(MathHelper.DegreesToRadians(this.Pitch)));
            front.Z = (float)(Math.Sin(MathHelper.DegreesToRadians(this.Yaw)) * Math.Cos(MathHelper.DegreesToRadians(this.Pitch)));

            this.Front = front.Normalized();
            this.Right = Vector3.Normalize(Vector3.Cross(this.Front, this.WorldUp));
            this.Up = Vector3.Normalize(Vector3.Cross(this.Right, this.Front));
        }

        public override Matrix4 GetProjection()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(this.Zoom), Constants.ASPECT, Constants.ZNEAR, Constants.ZFAR);
        }
        public override Matrix4 GetView()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

        public void ProcessKeyboard(CAMERA_MOVE_DIRECTION direction, float dt)
        {
            //make sure not to use if not if else
            float velocity = this.MovementSpeed * dt;

            if (direction == CAMERA_MOVE_DIRECTION.FORWARD)
            {
                this.Position += this.Front * velocity;
            }
            if (direction == CAMERA_MOVE_DIRECTION.BACKWARD)
            {
                this.Position -= this.Front * velocity;
            }

            if (direction == CAMERA_MOVE_DIRECTION.LEFT)
            {
                this.Position -= this.Right * velocity;
            }
            if (direction == CAMERA_MOVE_DIRECTION.RIGHT)
            {
                this.Position += this.Right * velocity;
            }
            if (direction == CAMERA_MOVE_DIRECTION.UP)
            {
                this.Position += this.Up * velocity;
            }
            if (direction == CAMERA_MOVE_DIRECTION.DOWN)
            {
                this.Position -= this.Up * velocity;
            }
        }
        public void ProcessMouseMovement(float xOffset, float yOffset, bool constrainPitch = true)
        {
            xOffset *= this.MouseSensitivity;
            yOffset *= this.MouseSensitivity;

            this.Yaw += xOffset;
            this.Pitch += yOffset;

            if (constrainPitch)
            {
                if (this.Pitch > 89f)
                {
                    this.Pitch = 89f;
                }
                if (this.Pitch < -89f)
                {
                    this.Pitch = -89f;
                }
            }
            this.UpdateCameraVectors();
        }
        public void ProcessMouseScroll(float yOffset)
        {
            if (this.Zoom >= 1f && this.Zoom <= 45f)
            {
                this.Zoom -= yOffset;
            }
            if (this.Zoom <= 1.0f)
            {
                this.Zoom = 1.0f;
            }
            if (this.Zoom >= 45f)
            {
                this.Zoom = 45f;
            }
        }

    }
}

