using System.Collections;

namespace Scuffle.Core;

public interface IBattleEventHandler
{
    public IEnumerator OnEvent(BattleContext context, IBattleEvent @event);
}