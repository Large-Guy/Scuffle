namespace Scuffle.Core.Events;

public record AddEffectEvent(BattleEntity On, IBattleAction Effect) : IBattleEvent;