using TeppichsTools.Logging;

namespace ARPG.Pawns.Enemy
{
    public class EnemyBase : Pawn
    {
        protected override void Kill()
        {
            DropLoot();

            Destroy(gameObject);

            void DropLoot() => EditorDebug.Log($"{name} is dropping Loot");
        }
    }
}