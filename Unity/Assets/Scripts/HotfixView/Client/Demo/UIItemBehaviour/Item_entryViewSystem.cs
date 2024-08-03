
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(Scroll_Item_entry))]
	public static partial class Scroll_Item_entryViewSystem 
	{
		[EntitySystem]
		private static void Awake(this Scroll_Item_entry self )
		{
		}

		[EntitySystem]
		private static void Destroy(this Scroll_Item_entry self )
		{
			self.DestroyWidget();
		}
	}
}
