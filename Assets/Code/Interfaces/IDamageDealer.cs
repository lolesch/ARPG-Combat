namespace ARPG.Combat
{
    public interface IDamageDealer
    {
        void DealDamage(IDamageTaker target, float damage);
    }
}