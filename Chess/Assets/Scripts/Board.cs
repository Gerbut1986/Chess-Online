using UnityEngine;
using Assets.Scripts;

public class Board : MonoBehaviour, ICreatable
{
    public GameObject menu;
    public GameObject styles;
    BoxSquares squares;
    BoxFigures figures;
    BoxPromotions promots;

    DragAndDrop dad;
    Game game;
    public Board()
    {
        squares = new BoxSquares(this);
        figures = new BoxFigures(this);
        promots = new BoxPromotions(this);
        game = new Game();
        // chess = new Chess("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NpPP/RNBQK2R w QK - 1 8");
        dad = new DragAndDrop(PickObject, DropObject);
    }
    public void Menu()
    {
        if (menu.activeSelf == true)
        {
            styles.SetActive(false);   // отключаем comboBox стилей
            menu.SetActive(false);     // Показываем панель меню
        }   
        else
            menu.SetActive(true);      // Скрываем панель меню          
    }
    public void Styles()
    {
        if (styles.activeSelf == true)
            styles.SetActive(false);   // отключаем
        else styles.SetActive(true);
    }
    public void Exit()
    {
       // if (styles.activeSelf == true)
            Application.Quit();
    }
    void Start() // Фигуры, в которые могут превращаться пешки
    {
        game.Init();
        squares.Init();
        figures.Init();
        promots.Init();
        ShowFigures();
        InvokeRepeating("Refresh", 2, 2);
    }
    void Refresh()
    {
        if (dad.state == DragAndDrop.State.drag) // Пока таскаеться фигура не нужно запрашивать обновление
            return;
        if (game.Refresh()) // Если метод Refresh() отработал(когда конец игры), то мы показываем все фигуры.
            ShowFigures();
    }
    public GameObject CreateGameObject(int x, int y, string pattern)
    {
        GameObject go = Instantiate(GameObject.Find(pattern)); // Для пустой клетки
        go.transform.position = Coords.GetVector(x, y);
        go.name = pattern;
        return go;
    }
    public void SetSprite(GameObject go, string source)
    {
        go.GetComponent<SpriteRenderer>().sprite =
                    GameObject.Find(source).GetComponent<SpriteRenderer>().sprite;
    }
    // Ф-ция для размещения всех фигур на доске:
    void ShowFigures()
    {
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                figures.SetPosition(x, y, squares);
                figures.SetSpriteAt(x, y, game.GetFigureAt(x, y));
            }
        squares.MarkSquaresFrom(game.GetMoves());
        promots.HidePromotionFigures();
    }
    // Update is called once per frame
    void Update()
    {
        dad.Action();
    }
    // После выполнения хода:
    void DropObject(Vector2 from, Vector2 to) // перетаскиваем фигуру
    {
        // Получаем координаты откуда и куда идет пешка:
        string e2 = Coords.GetSquare(from);
        string e4 = Coords.GetSquare(to);
        game.NextMove(e2, e4);   // выполнения след. хода
        if(game.isPromotionMove) // если пешка дошла до последней клетки
        {
            promots.ShowPromotionFigures(game.figure); // Показать окошко для выбора фигур
            return;
        }   
        // а если это не превращение.. тогда мы покажим все фигуры:
        ShowFigures(); // показываем фигуры
    }
    void PickObject(Vector2 from) // Взятие любой фигуры
    {
        if (game.isPromotionMove)      // Если у нас процесс превращение фигуры
        {
            game.NextPromotionMove(    // Сделали след. ход превращение с уже известной фигурой
                promots.GetPromotionFigures(Coords.GetX(from), Coords.GetY(from)));
            // Pd7d8Q  -  Фигура в которую должна превратиться пешка:
            ShowFigures(); 
            return;
        }
        squares.MarkSquaresTo(game.GetMoves(), Coords.GetSquare(from)); // e2
    }
}
