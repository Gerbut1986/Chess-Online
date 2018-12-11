using System;
using UnityEngine;

namespace Assets.Scripts
{
    static class Coords   // Берет на себя задачи трансформации
    {
        public static string GetSquare(Vector2 vector)
        {
            int x = GetX(vector);
            int y = GetY(vector);
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
                return GetSquare(x, y);
            return ""; // пусто если за пределы доски
        }
        public static string GetSquare(int x,int y)
        {
            return ((char)('a' + x)).ToString() + (y + 1).ToString();
        }
        public static Vector2 GetVector(int x, int y)
        {
            return new Vector2(2 * x, 2 * y);
        }
        public static int GetX(Vector2 vector)
        {
            return Convert.ToInt32(vector.x / 2);
        }
        public static int GetY(Vector2 vector)
        {
            return Convert.ToInt32(vector.y / 2);
        }
        public static int GetX(string square) // e2 -> e -> 4
        {
            return square[0] - 'a';
        }
        public static int GetY(string square) // e2 -> 2 -> 1
        {
            return square[1] - '1';
        }
    }
}
