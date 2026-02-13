using System.Collections;

namespace Scuffle.Core;

public abstract class BattleEntity
{
    private List<IBattleAction> _effects = [];
    public IBattleAction? Controller { get; protected set; }

    public IReadOnlyList<IBattleAction> Effects => _effects.ToList();
    
    public void AddEffect(IBattleAction effect)
    {
        _effects.Add(effect);
    }

    public void RemoveEffect(IBattleAction effect)
    {
        _effects.Remove(effect);
    }
    
    public bool HasEffect<T>() where T : IBattleAction => _effects.Any(e => e is T);
    
    public IEnumerator Run(BattleContext context)
    {
        if (Controller == null)
            yield break;
        yield return Controller.Run(context, this);
    }
}