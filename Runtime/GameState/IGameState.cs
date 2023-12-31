namespace YusamPackage
{
    public interface IGameState
    {
        public GameStateMachineDictionary GetGameStateMachine();
        public void Enter();
        public void Exit();
        public void Update();
    }
}
