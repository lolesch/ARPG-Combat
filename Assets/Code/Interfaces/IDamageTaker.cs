namespace ARPG.Combat
{
    public interface IDamageTaker
    {
        void SetCurrentHealth();
        void TakeDamage(float damage);
    }
}