namespace Scuffle.Core.Events;

public record EndTurnEvent(BattleEntity Entity) : IBattleEvent;