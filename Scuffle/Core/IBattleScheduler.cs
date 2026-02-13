using System.Collections;

namespace Scuffle.Core;

public interface IBattleScheduler
{
    public void Init(BattleContext context);

    public void AddEntity(BattleEntity entity)
    {
    }

    public void RemoveEntity(BattleEntity entity)
    {
    }

    public IEnumerator Next(BattleContext context);
}