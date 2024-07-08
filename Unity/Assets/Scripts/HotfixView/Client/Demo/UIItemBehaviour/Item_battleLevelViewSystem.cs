
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(Scroll_Item_battleLevel))]
	public static partial class Scroll_Item_battleLevelViewSystem 
	{
		[EntitySystem]
		private static void Awake(this Scroll_Item_battleLevel self )
		{
		}

		[EntitySystem]
		private static void Destroy(this Scroll_Item_battleLevel self )
		{
			self.DestroyWidget();
		}
	}
}
