using TeppichsTools.Logging;

namespace ARPG.Pawns.Enemy
{
    public class EnemyBase : Pawn
    {
        protected override void Kill()
        {
            DropLoot();

            base.Kill();

            void DropLoot() => EditorDebug.Log($"{name} is dropping Loot");
        }
    }
}