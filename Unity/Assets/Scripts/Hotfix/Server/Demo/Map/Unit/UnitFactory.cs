using System;
using Unity.Mathematics;

namespace ET.Server
{
    public static partial class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    
                    //读表初始化玩家属性
                    foreach (var config in PlayerNumericConfigCategory.Instance.GetAll())
                    {
                        if ( config.Value.BaseValue == 0 )
                        {
                            continue;
                        }

                        if ( config.Key < 3000 ) //小于3000的值都用加成属性推导
                        {
                            int baseKey = config.Key * 10 + 1;
                            numericComponent.SetNoEvent(baseKey,config.Value.BaseValue);
                        }
                        else
                        {
                            //大于3000的值 直接使用
                            numericComponent.SetNoEvent(config.Key,config.Value.BaseValue);
                        }
                    }
                    unitComponent.Add(unit);
                    return unit;
                }
                case UnitType.Monster:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(IdGenerater.Instance.GenerateId(), (int)id);
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.SetNoEvent(NumericType.MaxHp, unit.Config().MaxHP);
                    numericComponent.SetNoEvent(NumericType.Hp, unit.Config().MaxHP);
                    int damageValue = unit.Config().DamageValue;
                    numericComponent.SetNoEvent(NumericType.DamageValue, damageValue);
                    numericComponent.SetNoEvent(NumericType.IsAlive, 1);
                    
                    unitComponent.Add(unit);
                    return unit;
                }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }
    }
}