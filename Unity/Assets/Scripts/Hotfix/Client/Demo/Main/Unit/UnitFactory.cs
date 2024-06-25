using Unity.Mathematics;

namespace ET.Client
{
    public static partial class UnitFactory
    {
        public static Unit Create(Scene currentScene, UnitInfo unitInfo)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unitComponent.Add(unit);
	        
	        // unit.Position = unitInfo.Position;
	        // unit.Forward = unitInfo.Forward;
	        
	        //数值组件
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();

			foreach (var kv in unitInfo.KV)
			{
				numericComponent.Set(kv.Key, kv.Value);
			}
	        
	   //      unit.AddComponent<MoveComponent>();
	   //      if (unitInfo.MoveInfo != null)
	   //      {
		  //       if (unitInfo.MoveInfo.Points.Count > 0)
				// {
				// 	unitInfo.MoveInfo.Points[0] = unit.Position;
				// 	unit.MoveToAsync(unitInfo.MoveInfo.Points).Coroutine();
				// }
	   //      }

	        unit.AddComponent<ObjectWait>();

	        // unit.AddComponent<XunLuoPathComponent>();
	        
	        //告诉显示层现在可以创建游戏角色
	        EventSystem.Instance.Publish(unit.Scene(), new AfterUnitCreate() {Unit = unit});
            return unit;
        }
    }
}
