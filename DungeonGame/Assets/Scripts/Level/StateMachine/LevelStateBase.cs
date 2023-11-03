namespace LevelLogic
{
    public abstract class LevelStateBase
    {
        public abstract bool Condition();
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}