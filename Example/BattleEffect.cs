using System.Collections;
using Scuffle.Core;

namespace Example;

public abstract class BattleEffect : IBattleAction
{
    public int Duration;

    public IEnumerator Run(BattleContext context, BattleEntity entity)
    {
        Duration--;
        if(Duration <= 0)
            entity.RemoveEffect(this);
        yield break;
    }
}