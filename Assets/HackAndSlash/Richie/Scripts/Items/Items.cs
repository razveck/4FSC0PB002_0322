using UnityEngine;

namespace Richie
{
    public class Items : MonoBehaviour
    {
        public enum Item
        {
            none,
            coin,
            bone,
            egg,
            feather,
            plank,
            cheese,
            blueEgg,
            emerald,
            sapphire,
            amethyst
        }

        public Item thisItem;
    }
}
