using System.Collections;
using Scuffle.Core;

namespace Example;

public class PlayerController : IBattleAction
{
    public IEnumerator Run(BattleContext context, BattleEntity entity)
    {
        if (entity is not PlayerEntity player)
            throw new Exception("Player controller should only be ran on player entities");
        
        var result = Console.ReadLine();
        switch (result)
        {
            case "attack":
            {
                Console.WriteLine("Player attacks!");
                var enemy = context.Find(e => e is EnemyEntity);
                if (enemy == null) throw new Exception("No valid targets");
                
                yield return context.Invoke(new DealDamageEvent(enemy, 1));
                break;
            }
            case "defend":
            {
                Console.WriteLine("Player defends!");
                entity.AddEffect(new DefendEffect());
                break;
            }
        }
    }
}