using System.Collections;
using Scuffle.Core;

namespace Example;

public class DealDamageRule : IBattleRule
{
    public IEnumerator OnEvent(BattleContext context, IBattleEvent @event)
    {
        if (@event is not DealDamageEvent damageEvent) yield break;

        MyBattleEntity? battleEntity = damageEvent.Target as MyBattleEntity;

        if (battleEntity == null) throw new Exception("Target is not MyBattleEntity");
        
        Console.WriteLine($"{battleEntity.Name} takes {damageEvent.Damage} damage!");

        battleEntity.Hp -= damageEvent.Damage;
    }
}