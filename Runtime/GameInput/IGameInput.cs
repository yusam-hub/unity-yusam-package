namespace YusamPackage.GameInput
{
    public interface IGameInput
    {
        public GameInputProxy GetGameInputProxy();
        public bool HasGameInputProxy();
    }
}