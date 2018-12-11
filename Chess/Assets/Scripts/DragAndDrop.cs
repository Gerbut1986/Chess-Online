using UnityEngine;

namespace Assets.Scripts
{
    class DragAndDrop
    {
        public State state { get; private set; }
        GameObject item;
        Vector2 offset;
        Vector2 fromPosition, toPosition;

        public delegate void dePickObject(Vector2 from);
        public delegate void deDropObject(Vector2 from, Vector2 to);

        dePickObject PickObject;
        deDropObject DropObject; // save function on the same signatures
        public DragAndDrop(dePickObject PickObject, deDropObject DropObject)
        {
            this.PickObject = PickObject;
            this.DropObject = DropObject;
            item = null;
            state = State.none;
        }
        public enum State
        {
            none,
            drag,
        }
        public void Action()
        {
            switch (state)
            {
                case State.none:
                    if (IsMouseButtonPressed())
                        Pickup();
                    break;
                case State.drag:
                    if (IsMouseButtonPressed())
                        Drag();
                    else Drop();
                    break;
            }
        }
        bool IsMouseButtonPressed()
        {
            return Input.GetMouseButton(0);
        }
        void Pickup()
        {
            Vector2 clickPosition = GetClickPosition();
            Transform clickedItem = GetItemAt(clickPosition);
            clickedItem = GetItemAt(clickPosition);
            if (clickedItem == null)
                return;
            state = State.drag;
            item = clickedItem.gameObject;
            fromPosition = clickedItem.position;
            offset = fromPosition - clickPosition;
            PickObject(fromPosition);
        }
        Vector2 GetClickPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        Transform GetItemAt(Vector2 position)
        {
            RaycastHit2D[] figures = Physics2D.RaycastAll(position, position, 0.5f);
            if (figures.Length == 0)
                return null;
            return figures[0].transform;
        }
        void Drag()
        {
            item.transform.position = GetClickPosition() + offset;
        }
        void Drop()
        {
            toPosition = item.transform.position;
            DropObject(fromPosition, toPosition);
            item = null;
            state = State.none;
        }
    }
}
