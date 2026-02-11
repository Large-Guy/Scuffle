using System.Collections;
using Scuffle.Core;
using Scuffle.Core.Events;

namespace Example;

public class TurnBasedScheduler : IBattleScheduler
{
    private record Turn
    {
        public BattleEntity Entity;
        public int Initiative;
        public int NextTime;

        public Turn(BattleEntity entity, int nextTime, int initiative)
        {
            Entity = entity;
            NextTime = nextTime;
            Initiative = initiative;
        }
    }
    private PriorityQueue<Turn, int> _order = new();

    private List<BattleEntity> _preview = [];
    
    private BattleEntity? _current = null;
    
    public IReadOnlyList<BattleEntity> Preview => _preview;
    
    public BattleEntity? Current => _current;

    private BattleEntity Advance()
    {
        var next = _order.Dequeue();
        _preview.Add(next.Entity);
        
        if(_preview.Count > 10) _preview.RemoveAt(0);
        
        if(next.Entity is not MyBattleEntity player) return next.Entity;

        var newTime = next.NextTime + next.Initiative;
        
        _order.Enqueue(new Turn(next.Entity, newTime, next.Initiative), newTime);
        
        return next.Entity;
    }
    
    public void Init(BattleContext context)
    {
        _order = new PriorityQueue<Turn, int>();
        _preview = [];

        foreach (var entity in context.Entities)
        {
            if(entity.Controller == null) continue;
            if(entity is not MyBattleEntity player) continue;
            var initiative = 20-player.Speed;
            _order.Enqueue(new Turn(entity, 0, initiative), 0);
        }

        for (var i = 0; i < 10; i++)
        {
            Advance();
        }
    }
    
    public IEnumerator Next(BattleContext context)
    {
        _current = Preview[0];
        Advance();
        
        foreach (var effect in _current.Effects)
        {
            yield return effect.Run(context, _current);
        }
        
        yield return context.Invoke(new BeginTurnEvent(_current));
        yield return _current.Run(context);
        yield return context.Invoke(new EndTurnEvent(_current));
    }
}