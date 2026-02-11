using System.Collections;

namespace Scuffle.Core;

public interface IBattleAction
{
    public IEnumerator Run(BattleContext context, BattleEntity entity);
}