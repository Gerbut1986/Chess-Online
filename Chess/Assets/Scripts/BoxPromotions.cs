using UnityEngine;

namespace Assets.Scripts
{
    class BoxPromotions : Box // Робота с превращениями
    {
        const string proFigures = "QRBN"; // перечислим Белые фигуры
        const int minX = 2;               //  откуда мы начинаем отображать фигуры, предназнач. для первращение
        const int whiteY = 8;
        const int blackY = -1;
        public BoxPromotions(ICreatable creator) : base(creator) { }
        public void Init()
        {
            // White figures:
            for (int x = minX; x < minX + proFigures.Length; x++)
                Create(x, whiteY, GetWhiteProFigure(x));
            // Black figures:
            for (int x = minX; x < minX + proFigures.Length; x++)
                Create(x, blackY, GetBlackProFigure(x));
        }
        public void HidePromotionFigures() // Метод, который скрывает все фигуры (для обмена на пешку)
        {
            foreach (GameObject pro in list.Values)
                SetSpriteFor(pro, ".");
        }
        public void ShowPromotionFigures(string pawn) // Метод, который показывает все фигуры (для обмена на пешку)
        {
            //HidePromotionFigures(); // Hide all figures
            if (pawn == "P") // if white pawn:
                for (int x = minX; x < minX + proFigures.Length; x++)
                    SetSpriteAt(x, whiteY, GetWhiteProFigure(x));
            if (pawn == "p") // if black pawn:
                for (int x = minX; x < minX + proFigures.Length; x++)
                    SetSpriteAt(x, blackY, GetBlackProFigure(x));
        }
        public string GetPromotionFigures(int x, int y)
        {
            if (y == whiteY) return GetWhiteProFigure(x);
            if (y == blackY) return GetBlackProFigure(x);
            return "";
        }
        string GetWhiteProFigure(int x)
        {
            if (x >= minX && x < minX + proFigures.Length)
                return proFigures.Substring(x - minX, 1);
            return "";
        }
        string GetBlackProFigure(int x)
        {
            return GetWhiteProFigure(x).ToLower(); // Приводим к нижнему регистру:
        }
    }
}
