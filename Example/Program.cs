using System.Collections;
using Scuffle.Core;
using Scuffle.Core.Coroutine;

namespace Example;

public class PlayerEntity : MyBattleEntity
{
    public PlayerEntity(string name)
    {
        Name = name;
        SetController(new PlayerController());
    }
}

public class EnemyEntity : MyBattleEntity
{
    public EnemyEntity(string name, IBattleAction controller)
    {
        Name = name;
        SetController(controller);
    }
}

public class ZombieController : IBattleAction
{
    public IEnumerator Run(BattleContext context, BattleEntity entity)
    {
        Console.WriteLine($"{((EnemyEntity)entity).Name} is attacking!");
        var target = context.Find(e => e is PlayerEntity);
        yield return null;
        if (target == null) throw new Exception("No valid targets");
        yield return context.Invoke(new DealDamageEvent(target, 1));
        yield break;
    }
}

class Program
{
    static void Main(string[] args)
    {
        BattleContext.Preset preset = new BattleContext.Preset()
            .WithScheduler(new TurnBasedScheduler())
            .WithRule(new UpdateDisplayRule())
            .WithRule(new DefendRule())
            .WithRule(new DealDamageRule())
            .WithRule(new WinLoseConditionRule())
            .WithRule(new WinLoseRule());
        
        BattleContext context = preset.Build();

        var player = new PlayerEntity("Player")
        {
            Speed = 1
        };
        var zombie = new EnemyEntity("Zombie", new ZombieController())
        {
            Speed = 5
        };

        context.AddEntity(player);
        context.AddEntity(zombie);

        CoroutineRunner runner = new CoroutineRunner();
        
        runner.Start(context.Begin());

        while (true)
        {
            runner.Update();
        }
    }
}