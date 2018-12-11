namespace Assets.Scripts
{
    class BoxFigures : Box   
    {
        public BoxFigures(ICreatable creator) : base(creator) { }
        public void Init()
        {
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                    Create(x, y, "p");  
        }
    }
}
