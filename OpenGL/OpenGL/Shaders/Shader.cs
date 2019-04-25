using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System.IO;
using System.Collections.Generic;
using OpenGL.Entities;

namespace OpenGL
{
    public class Shader
    {
        public Shader(List<ShaderInfo> shaders)
        {
            Ids = new List<int>();
            Program = GL.CreateProgram();

            int shaderId = -1;
            foreach (ShaderInfo shader in shaders)
            {
                shaderId = Compile(shader.type, shader.name);
                Ids.Add(shaderId);
                GL.AttachShader(Program, shaderId);
            }

            GL.LinkProgram(Program);
            GetLinkingStatus();
        }
        private int Program;
        List<int> Ids;

        private void GetLinkingStatus()
        {
            var log = GL.GetProgramInfoLog(Program);
            if (!string.IsNullOrEmpty(log))
            {
                Console.WriteLine("failed to link program " + '\n' + log);
            }
        }
        protected void BindAttribute(int attribute, String variableName)
        {
            GL.BindAttribLocation(Program, attribute, variableName);
        }
        public void Start()
        {
            GL.UseProgram(Program);
        }
        public void Stop()
        {
            GL.UseProgram(0);
        }
        public void CleanUp()
        {
            Stop();
            foreach (int id in Ids)
            {
                GL.DetachShader(Program, id);
                GL.DeleteShader(id);
            }
            GL.DeleteProgram(Program);
        }
        private int Compile(ShaderType type, string path)
        {
            string source = File.ReadAllText(path);
            int id = GL.CreateShader(type);
            GL.ShaderSource(id, source);
            GL.CompileShader(id);
            string log = GL.GetShaderInfoLog(id);
            if (!string.IsNullOrEmpty(log))
            {
                Console.WriteLine("failed to comile sahder  " + path + '\n' + log);
            }
            return id;
        }

        #region load uniforms
        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(Program, name);
        }
        #endregion

        #region send variables to gpu
        public void SetFloat(int location, float val)
        {
            GL.Uniform1(location, val);
        }
        public void SetDouble(int location, double val)
        {
            GL.Uniform1(location, val);
        }
        public void SetInt(int location, int val)
        {
            GL.Uniform1(location, val);
        }
        public void SetBool(int location, bool val)
        {
            GL.Uniform1(location, val ? 1 : 0);
        }
        public void SetVector2(int location, Vector2 val)
        {
            GL.Uniform2(location, val);
        }
        public void SetVector3(int location, Vector3 val)
        {
            GL.Uniform3(location, val);
        }
        public void SetMatrix4(int location, Matrix4 val)
        {
            GL.UniformMatrix4(location, false, ref val);
        }
        #endregion
    }
}
