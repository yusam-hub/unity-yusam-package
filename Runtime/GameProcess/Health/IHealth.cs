namespace YusamPackage
{
    public interface IHealth
    {
        public void ResetHealth();
        public float GetHealth();
        public void PlusHealth(float volume);
        public void MinusHealth(float volume);
    }
}