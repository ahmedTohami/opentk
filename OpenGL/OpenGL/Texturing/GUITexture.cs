using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL
{
    public class GUITexture
    {
        public GUITexture(int id, Vector2 position, Vector2 scale)
        {
            Id = id;
            this.Position = position;
            this.Scale = scale;
        }
        public int Id { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Scale { get; private set; }
    }
}
