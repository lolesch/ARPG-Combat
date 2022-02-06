namespace ARPG.Enums
{
    public enum CrowdControl
    {
        Stasis = 1,         // unable to move, attack, or cast while being untargetable and invulnerable
        Charm = 2,          // unable to move, attack, or cast and move toward the source with reduced movement speed
        Fear = 3,           // unable to move, attack, or cast and move away from the source with reduced movement speed
        Taunt = 4,          // unable to move, attack, or cast and attempt to basic attack the source
        Stun = 5,           // unable to move, attack, or cast 
        KnockUp = 6,        // unable to move, attack, or cast 
        Sleep = 7,          // unable to move, attack, or cast as long as it doesn't get damaged

        Root = 8,           // unable to move,         or cast mobility spells
        Disarm = 9,         // unable to       attack
        Silence = 10,        // unable to                  cast

        Blind = 11,          // will miss its attacks. Miss occurs on-hit, and does not prevent attacks from being declared
        Disrupt = 12,        // will interrupt channeled and charged abilities
        Confusion = 13,      // will attack allies/fight on the caster's side

        Slow = 14,           // reduced movement speed
        Exhaust = 15,        // reduced attack speed
        Weaken = 16,         // reduced damage
        Vulnerable = 17,     // reduced resistances

        Nearsight = 18,      // reduced sight radius and loses allied vision
    }
}