using System.Collections;
using Scuffle.Core;

namespace Example;

public class DefendRule : IBattleRule
{
    public IEnumerator OnEvent(BattleContext context, IBattleEvent @event)
    {
        if (@event is not DealDamageEvent damageEvent) yield break;
        
        if(!damageEvent.Target.HasEffect<DefendEffect>()) yield break;

        damageEvent.Damage = 0;
    }
}