using System.Collections;
using Scuffle.Core;
using Scuffle.Core.Events;

namespace Example;

public class UpdateDisplayRule : IBattleRule
{
    public IEnumerator OnEvent(BattleContext context, IBattleEvent @event)
    {
        switch (@event)
        {
            case BeginTurnEvent:
            {
                Console.Clear();
                MyBattleEntity? current = ((TurnBasedScheduler)context.Scheduler).Current as MyBattleEntity;
                if (current == null) throw new Exception("No current turn");
                
                Console.Write($" > [{current.Name}] ");
                var previews = ((TurnBasedScheduler)context.Scheduler).Preview;
                for (var i = 0; i < previews.Count; i++)
                {
                    var preview = previews[i];
                    Console.ForegroundColor = preview is PlayerEntity ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.Write($"{((MyBattleEntity)preview).Name}");
                    Console.ResetColor();
                    if (i != previews.Count - 1) Console.Write(", ");
                }

                Console.WriteLine();
                
                Console.WriteLine($"{current.Name}'s Turn!");
                Console.WriteLine($"Stats -> HP {current.Hp}/{current.MaxHp}");
                Console.Write("Effects: ");
                foreach (var effect in current.Effects)
                {
                    var battleEffect = effect as BattleEffect;
                    if (battleEffect == null) throw new Exception("Expected BattleEffect");
                    Console.Write($"{battleEffect.GetType()} ({battleEffect.Duration})");
                    if(effect != current.Effects.Last()) Console.Write(", ");
                }
                Console.WriteLine();
                
                break;
            }
            case EndTurnEvent:
            {
                yield return null;
                break;
            }
        }
    }
}