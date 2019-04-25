
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL
{
    public class Constants
    {

        //=================== player ======================
        public static  float RUN_SPEED = 100;
        public static  float TURN_SPEED = 50;
        public static  float GRAVITY = -50;
        public static  float JUMP_POWER = 30;


        //================== camera ==============================

        public static float FASTSPEED = 4f;
        public static float ZNEAR = 0.01f;
        public static float ZFAR = 2000.0f;
        public static float FOV = 70f;
        public static float ASPECT = 1f;


        //============== third person camara =============================
        public static float ZOOM_SENSITIVITY = 5f;
        public static float ROTATION_SENSITIVITY = 0.3f;

        

        public static float SkyRed  => 0;
        public static float SkyGreen=> 0;
        public static float SkyBlue => 0;

        public static float TERRAIN_HEIGHT = 0;
    }
    
}
