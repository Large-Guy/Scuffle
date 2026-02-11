using System.Collections;
using Scuffle.Core;
using Scuffle.Core.Events;

namespace Example;

public class WinLoseRule : IBattleRule
{
    private BattleResult _result;
    public IEnumerator OnEvent(BattleContext context, IBattleEvent @event)
    {
        switch (@event)
        {
            case BeginTurnEvent beginTurnEvent:
            {
                _result = BattleResult.None;
                break;
            }
            case EndTurnEvent endTurnEvent:
            {
                if (_result != BattleResult.None)
                {
                    context.End();
                }

                break;
            }
            case WinEvent winEvent:
            {
                if (_result == BattleResult.Defeat) yield break;
                _result = BattleResult.Victory;
                break;
            }
            case LoseEvent loseEvent:
            {
                _result = BattleResult.Defeat;
                break;
            }
        }
    }
}