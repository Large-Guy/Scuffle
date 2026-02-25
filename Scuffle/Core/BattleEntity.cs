using System.Collections;
using Scuffle.Core.Events;

namespace Scuffle.Core;

public abstract class BattleEntity
{
    private List<IBattleAction> _effects = [];
    public IBattleAction? Controller { get; protected set; }

    public IReadOnlyList<IBattleAction> Effects => _effects.ToList();
    
    public IEnumerator AddEffect(BattleContext context, IBattleAction effect)
    {
        yield return context.Invoke(new AddEffectEvent(this, effect));
        _effects.Add(effect);
    }

    public IEnumerator RemoveEffect(BattleContext context, IBattleAction effect)
    {
        if(!_effects.Contains(effect))
            yield break;
        
        yield return context.Invoke(new RemoveEffectEvent(this, effect));
        _effects.Remove(effect);
    }

    public T? GetEffect<T>() where T : IBattleAction
    {
        return _effects.OfType<T>().FirstOrDefault();
    }
    
    public bool HasEffect<T>() where T : IBattleAction => _effects.Any(e => e is T);
    
    public IEnumerator Run(BattleContext context)
    {
        if (Controller == null)
            yield break;
        yield return Controller.Run(context, this);
    }
}