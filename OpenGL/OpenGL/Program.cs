namespace OpenGL
{
    class Program
    {
        static void Main(string[] args)
        {
            MainGameLoop window = MainGameLoop.GetInstance();
            window.Run(60);
        }
    }
}
