using UnityEngine;

namespace Richie
{
    public class PlayerBag : MonoBehaviour
    {
        public event ItemCollect OnCollect;
        public delegate void ItemCollect(Items.Item item);

        public void Collected(Items.Item item)
        {
            switch (item)
            {
                case Items.Item.coin:
                    OnCollect?.Invoke(Items.Item.coin);
                    break;

                case Items.Item.bone:
                    OnCollect?.Invoke(Items.Item.bone);
                    break;

                case Items.Item.egg:
                    OnCollect?.Invoke(Items.Item.egg);
                    break;

                case Items.Item.feather:
                    OnCollect?.Invoke(Items.Item.feather);
                    break;

                case Items.Item.plank:
                    OnCollect?.Invoke(Items.Item.plank);
                    break;

                case Items.Item.cheese:
                    OnCollect?.Invoke(Items.Item.cheese);
                    break;

                case Items.Item.blueEgg:
                    OnCollect?.Invoke(Items.Item.blueEgg);
                    break;

                case Items.Item.emerald:
                    OnCollect?.Invoke(Items.Item.emerald);
                    break;

                case Items.Item.sapphire:
                    OnCollect?.Invoke(Items.Item.sapphire);
                    break;

                case Items.Item.amethyst:
                    OnCollect?.Invoke(Items.Item.amethyst);
                    break;

                default:
                    Debug.Log("unknown item received");
                    break;
            }
        }
    }
}
