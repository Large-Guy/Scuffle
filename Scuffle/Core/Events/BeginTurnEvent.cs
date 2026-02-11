namespace Scuffle.Core.Events;

public record BeginTurnEvent(BattleEntity Entity) : IBattleEvent;