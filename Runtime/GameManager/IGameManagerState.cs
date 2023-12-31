namespace YusamPackage
{
    public interface IGameManagerState
    {
        public GameManager.GameManagerStateEnum GetGameManagerStateEnum();
        public void Enter();
        public void Exit();
        public void Update();
        public bool IsFinished();
    }
}