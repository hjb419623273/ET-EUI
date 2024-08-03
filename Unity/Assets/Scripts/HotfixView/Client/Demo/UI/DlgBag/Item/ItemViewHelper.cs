using UnityEngine;

namespace ET.Client
{
    [FriendOfAttribute(typeof(ET.Item))]
    public static class ItemViewHelper
    {
        public static Color ItemQualityColor(this Item item)
        {
            ItemQuality quality = (ItemQuality)item.Quality;
            switch (quality)
            {
                case ItemQuality.General:
                    return Color.white;
                case ItemQuality.Good:
                    return Color.green;
                case ItemQuality.Excellent:
                    return Color.blue;
                case ItemQuality.Epic:
                    return Color.magenta;
                case ItemQuality.Legend:
                    return new Color(225.0f / 255, 128.0f / 255, 0.0f);
            }
            return Color.black;
        }
    }
}