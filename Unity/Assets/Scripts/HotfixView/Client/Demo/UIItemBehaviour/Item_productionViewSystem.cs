
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(Scroll_Item_production))]
	public static partial class Scroll_Item_productionViewSystem 
	{
		[EntitySystem]
		private static void Awake(this Scroll_Item_production self )
		{
		}

		[EntitySystem]
		private static void Destroy(this Scroll_Item_production self )
		{
			self.DestroyWidget();
		}
	}
}
