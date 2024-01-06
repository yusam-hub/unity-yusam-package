namespace YusamPackage
{
    public interface IHealth
    {
        public bool IsHealthZero();
        public bool IsHealthMax();
        public float GetHealth();
        public void ResetHealth();
        public void PlusHealth(float volume);
        public void MinusHealth(float volume);
    }
}