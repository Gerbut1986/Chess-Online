namespace ChessRules_ClassLibrary_
{
    enum Color
    {
        none,
        white,
        black
    }
    static class ColorMethod
    {
        public static Color FlipColor(this Color color)
        {
            if (color == Color.black) return Color.white;
            if (color == Color.white) return Color.black;
            return color;
        }
    }
}
