namespace ET
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

    public struct RefreshEquipShowItems
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

    public struct RefreshItemPopUp
    {
        public Item Item;
        public ItemContainerType ItemContainerType;
    }
    
    public struct ChangeEquipItem
    {
        public Unit Unit;
        public Item Item;
        public EquipOp EquipOp;
    }

    public struct MakeQueueOver
    {
        [StaticField]
        public static readonly MakeQueueOver Instance = new MakeQueueOver();
    }
    
    public struct RefreshMakeEqueue
    {
            
    }
    
    public struct MakeProdutionOver
    {
        public Unit Unit;
        public int ProductionConfigId;
    }
    
    public struct UpdateTaskInfo
    {
        public Scene Scene;
    }
    
    public struct BattleWin
    {
        public Unit Unit;
        public int  LevelId;
    }
    
    public struct UpdateChatInfo
    {
        public Scene Scene;
    }
}