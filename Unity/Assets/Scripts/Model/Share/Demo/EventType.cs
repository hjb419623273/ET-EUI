namespace ET.Client
{
    public struct SceneChangeStart
    {
    }
    
    public struct SceneChangeFinish
    {
    }
    
    public struct AfterCreateClientScene
    {
    }
    
    public struct AfterCreateCurrentScene
    {
    }

    public struct AppStartInitFinish
    {
    }

    public struct LoginFinish
    {
    }

    public struct EnterMapFinish
    {
    }

    public struct AfterUnitCreate
    {
        public Unit Unit;
    }

    public struct RefreshRoleInfo
    {
        
    }
    
    public struct AdventureRoundReset
    {
        public Unit Unit;
    }
    
    public struct AdventureBattleRoundView
    {
        public Unit AttackUnit;
        public Unit TargeUnit;
    }
    
    public struct AdventureBattleRound
    {
        public Unit AttackUnit;
        public Unit TargeUnit;
    }

    public struct AdventureBattleOver
    {
        public Unit WinUnit;
    }

    public struct AdventureBattleReport
    {
        public BattleRoundResult BattleRoundResult;
        public int Round;
    }
    public struct ShowAdventureHpBar
    {
        public Unit Unit;
        public bool isShow;
    }

    public struct ShowDamageValueView
    {
        public Unit TargeUnit;
        public long DamamgeValue;
    }
}