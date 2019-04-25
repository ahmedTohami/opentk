using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL.Entities;
using OpenTK;
using OpenTK.Input;

namespace OpenGL
{

    public class Player : Entity
    {

        private float currentSpeed = 0;
        private float currentTurnSpeed = 0;
        private float upwardsSpeed = 0;

        private bool isInAir = false;

        public Player(Vector3 position, Vector3 rotation, Vector3 scale, RawModel rawModel ) : base(position, rotation, scale, rawModel)
        { }

        public void move(float frameTimeSeconds , Terrain terrain)
        {
            checkInputs();
            Rotate(0, currentTurnSpeed * frameTimeSeconds, 0);
            float distance = currentSpeed * frameTimeSeconds;
            float dx = (float)(distance * Math.Sin(MathHelper.DegreesToRadians(Transformations.Rotation.Y)));
            float dz = (float)(distance * Math.Cos(MathHelper.DegreesToRadians(Transformations.Rotation.Y)));

            Transformations.Position = new Vector3(Transformations.Position.X + dx, Transformations.Position.Y, Transformations.Position.Z + dz);

            upwardsSpeed += Constants.GRAVITY * frameTimeSeconds;
            Translate(0, upwardsSpeed * frameTimeSeconds, 0);
            float terrainHeight = terrain.GetTerrainHeight(Transformations.Position.X ,Transformations.Position.Z);
            if (Transformations.Position.Y < terrainHeight)
            {
                upwardsSpeed = 0;
                isInAir = false;
                Transformations.Position = new Vector3(Transformations.Position.X, terrainHeight, Transformations.Position.Z);
            }
        }

        private void jump()
        {
            if (!isInAir)
            {
                this.upwardsSpeed = Constants.JUMP_POWER;
                isInAir = true;
            }
        }

        private void checkInputs()
        {

            if (Keyboard.GetState().IsKeyDown(Key.W) || Keyboard.GetState().IsKeyDown(Key.Up))
            {
                this.currentSpeed = Constants.RUN_SPEED;
            }
            else if (Keyboard.GetState().IsKeyDown(Key.S) || Keyboard.GetState().IsKeyDown(Key.Down))
            {
                this.currentSpeed = -Constants.RUN_SPEED;
            }
            else
            {
                this.currentSpeed = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Key.D) || Keyboard.GetState().IsKeyDown(Key.Right))
            {
                this.currentTurnSpeed = -Constants.TURN_SPEED;
            }
            else if (Keyboard.GetState().IsKeyDown(Key.A) || Keyboard.GetState().IsKeyDown(Key.Left))
            {
                this.currentTurnSpeed = Constants.TURN_SPEED;
            }
            else
            {
                this.currentTurnSpeed = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Space))
            {
                jump();
            }

        }


    }
}
