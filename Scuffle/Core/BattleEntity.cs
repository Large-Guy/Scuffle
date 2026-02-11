using System.Collections;

namespace Scuffle.Core;

public abstract class BattleEntity
{
    private List<IBattleAction> _effects = [];
    private List<IBattleAction> _actions = [];
    private IBattleAction? _controller;

    public IReadOnlyList<IBattleAction> Effects => _effects.ToList();
    public IReadOnlyList<IBattleAction> Actions => _actions;
    public IBattleAction? Controller => _controller;

    protected void AddAction(IBattleAction action)
    {
        _actions.Add(action);
    }
    
    protected void SetController(IBattleAction controller)
    {
        _controller = controller;
    }
    
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
        if (_controller == null)
            yield break;
        yield return _controller.Run(context, this);
    }
}