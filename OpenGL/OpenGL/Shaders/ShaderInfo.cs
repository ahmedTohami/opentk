using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL
{
    public class ShaderInfo
    {
        public ShaderInfo(string name, ShaderType type)
        {
            this.name = name;
            this.type = type;
        }

        public string name  { get; set; }
        public ShaderType type { get; set; }
    }
}
