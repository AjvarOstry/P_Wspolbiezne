namespace TP.ConcurrentProgramming.Data
{
    public class Box
    {
        public Box(double width, double height)
        {
            Width  = width;
            Height = height;
        }

        public double Width  { get; }
        public double Height { get; }

        public bool HitsVerticalWall(double x)   => x <= 0 || x >= Width;
        public bool HitsHorizontalWall(double y) => y <= 0 || y >= Height;
    }
}