namespace Scuffle.Core.Events;

public record RemoveEffectEvent(BattleEntity On, IBattleAction Effect) : IBattleEvent;