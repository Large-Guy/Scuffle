using System.Collections;
using Scuffle.Core;
using Scuffle.Core.Events;

namespace Example;

public class WinLoseConditionRule : IBattleRule
{
    public IEnumerator OnEvent(BattleContext context, IBattleEvent @event)
    {
        if (@event is not BeginTurnEvent) yield break;
        
        foreach (var entity in context.FindAll(entity => entity is PlayerEntity))
        {
            var player = (PlayerEntity) entity;
            if (player.Hp <= 0) yield return context.Invoke(new WinEvent());
        }
        foreach (var entity in context.FindAll(entity => entity is EnemyEntity))
        {
            var enemy = (EnemyEntity) entity;
            if (enemy.Hp <= 0) yield return context.Invoke(new LoseEvent());
        }
    }
}