using Scuffle.Core;

namespace Example;

public class DealDamageEvent(BattleEntity target, int damage) : IBattleEvent
{
    public BattleEntity Target = target;
    public int Damage = damage;
}