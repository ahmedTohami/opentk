using System;
namespace OpenGL.Entities
{
    public static class CameraFactory
    {
        public static EngineCamera GetCamera(Type typeOfCamera, Player Player = null)
        {
            if (typeOfCamera == typeof(Camera))
            {
                return Camera.GetInstance();
            }
            if (typeOfCamera == typeof(ModernCamera))
            {
                return ModernCamera.GetInstance();
            }
            if (typeOfCamera == typeof(ThirdPersonCamera))
            {
                return ThirdPersonCamera.GetInstance(Player);
            }
            return null;
        }
    }
}
