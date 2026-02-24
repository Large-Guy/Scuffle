using System.Collections;
using Scuffle.Core.Events;

namespace Scuffle.Core;

public class BattleContext
{
    public class Preset
    {
        private IBattleScheduler? _scheduler = null;
        private List<IBattleRule> _rules = [];
        
        public Preset WithScheduler(IBattleScheduler scheduler)
        {
            _scheduler = scheduler;
            return this;
        }

        public Preset WithRule(IBattleRule rule)
        {
            _rules.Add(rule);
            return this;
        }

        public BattleContext New()
        {
            if(_scheduler == null) throw new InvalidOperationException("Scheduler is required");

            return new BattleContext(_scheduler)
            {
                _rules = _rules
            };
        }
    }
    
    private List<BattleEntity> _entities = [];
    private List<IBattleRule> _rules = [];
    
    private IBattleScheduler _scheduler;
    
    private bool _isRunning = true;

    public IBattleScheduler Scheduler => _scheduler;
    public IReadOnlyList<BattleEntity> Entities => _entities;
    
    private BattleContext(IBattleScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public IEnumerator Begin()
    {
        if(!_isRunning) throw new InvalidOperationException("Battle is already ended");
        yield return Invoke(new BeginBattleEvent());
        _scheduler.Init(this);
        while (_isRunning)
        {
            yield return _scheduler.Next(this);
        }
    }

    public IEnumerator End()
    {
        yield return Invoke(new EndBattleEvent());
        _isRunning = false;
    }
    
    public void AddEntity(BattleEntity entity)
    {
        _entities.Add(entity);
        _scheduler.AddEntity(entity);
    }
    
    public void RemoveEntity(BattleEntity entity)
    {
        _entities.Remove(entity);
        _scheduler.RemoveEntity(entity);
    }
    
    public BattleEntity? Find(Func<BattleEntity, bool> predicate) => _entities.FirstOrDefault(predicate);

    public List<BattleEntity> FindAll(Func<BattleEntity, bool> predicate)
    {
        List<BattleEntity> entities = [];
        foreach (var entity in _entities)
        {
            if(predicate.Invoke(entity))
                entities.Add(entity);
        }

        return entities;
    }

    public IEnumerator Invoke(IBattleEvent @event)
    {
        foreach (var t in _rules)
        {
            yield return t.OnEvent(this, @event);
        }
    }
}