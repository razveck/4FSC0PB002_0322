using System.Collections.Generic;
using UnityEngine;

namespace Richie
{
    public class LootTable : MonoBehaviour
    {
        [System.Serializable]
        public class Category
        {
            public string Name;
            public int Weight;
            public Color Color; 
            public Vector2Int Amount;
            public GameObject[] Items;
            internal Vector2 range;
        }

        [SerializeField, NonReorderable] 
        private Category[] _categories;

        private void Start()
        {
            AssignValues();
        }

        private void AssignValues()
        {   
            float totalWeight = 0f;
            List<float> weights = new();

            if (_categories.Length <= 0) return;

            // add up all drop chances //
            for (int i = 0; i < _categories.Length; i++)
            {
                weights.Add(_categories[i].Weight);
                totalWeight += _categories[i].Weight; 
            }

            float ratio = (100 / totalWeight);
            for (int i = 0; i < weights.Count; i++)
            {   // normalize their values //
                weights[i] = weights[i] * ratio;
            }

            float start, end = 0f;
            for (int i = 0; i < weights.Count; i++)
            {   // give each tier a range on a number line //
                start = end;
                end += weights[i];
                _categories[i].range = new(start, end);
            }
        }

        private Category GetCategory()
        {
            float number = Random.Range(0f, 100f);
            foreach (Category category in _categories)
            {   // generate random number to decide a category // 
                if (number >= category.range.x && number <= category.range.y) 
                    return category;
            }

            return null;
        }

        public List<GameObject> GetLoot()
        {   // get loot from the list of items //
            List<GameObject> loot = new();
            Category category = GetCategory();
            int amount = Random.Range(category.Amount.x, category.Amount.y + 1);

            for (int i = 0; i < Mathf.Abs(amount); i++)
            {
                int index = Random.Range(0, category.Items.Length);
                loot.Add(category.Items[index]);
            }

            return loot;
        }

        [ContextMenu("Random Loot")]
        private void RandomLoot()
        {
            List<GameObject> loot = GetLoot();
            foreach (var item in loot) print(item.name);
        }
    }
}

