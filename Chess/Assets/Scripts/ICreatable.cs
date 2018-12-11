using UnityEngine;

namespace Assets.Scripts
{
    interface ICreatable
    {
        GameObject CreateGameObject(int x, int y, string pattern);
        void SetSprite(GameObject go, string source);
    }
}
