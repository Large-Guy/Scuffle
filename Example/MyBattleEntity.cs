using Scuffle.Core;

namespace Example;

public abstract class MyBattleEntity : BattleEntity
{
    public string Name = "";

    public int MaxHp = 5;
    
    private int _hp = 5;

    public int Speed = 0;

    public int Hp
    {
        get => _hp;
        set
        {
            _hp = Math.Min(MaxHp, value);
        }
    }
}