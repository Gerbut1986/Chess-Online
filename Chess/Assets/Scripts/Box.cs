using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    abstract class Box   // Контейнер для наших словарей
    {
        ICreatable creator;
        protected Dictionary<string, GameObject> list { get; private set; }

        public Box(ICreatable creator)
        {
            this.creator = creator;
            list = new Dictionary<string, GameObject>();
        }
        public void Create(int x, int y, string pattern)
        {
            GameObject go = creator.CreateGameObject(x, y, pattern);
            list[x + "," + y] = go;
        }
        public GameObject GetGameObject(int x, int y)
        {
            return list[x + "," + y];
        }
        public void SetPosition(int x, int y, Box form)
        {
            GetGameObject(x, y).transform.position =
       form.GetGameObject(x, y).transform.position;
        }
        public void SetSpriteAt(int x, int y, string source)
        {
            SetSpriteFor(GetGameObject(x, y), source);
        }
        public void SetSpriteFor(GameObject go, string source)
        {
            if (go.name == source) return;
            creator.SetSprite(go, source);
            go.name = source;
        }
    }
}
