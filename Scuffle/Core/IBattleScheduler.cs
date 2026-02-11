using System.Collections;

namespace Scuffle.Core;

public interface IBattleScheduler
{
    public void Init(BattleContext context);
    public IEnumerator Next(BattleContext context);
}