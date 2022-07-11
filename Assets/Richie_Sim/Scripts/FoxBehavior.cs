using System.Collections.Generic;
using UnityEngine;

namespace Richie.Simulation
{
    public class FoxBehavior : AnimalBase
    {
        [Header("Fox Reproduction")]
        public int creepScore = 0;
        public int amountToGrow = 5;

        public GameObject prey;

        protected override void Move()
        {
            Eat();
            base.Move();
        }

        protected override void FindTarget()
        {
            if (prey == null)
            {
                if (spawner.rabbits.Count > 0)
                {
                    MinDistance(spawner.rabbits);
                    target = prey.GetComponent<AnimalBase>().position;
                }
                else base.FindTarget();
            } else target = prey.GetComponent<AnimalBase>().position;
        }

        private void MinDistance(List<GameObject> allTargets)
        {
            float temp = float.MaxValue;
            for (int i = 0; i < allTargets.Count; i++)
            {
                float a = Vector2.Distance(transform.position, allTargets[i].transform.position);
                if (Mathf.Abs(a) < temp) prey = allTargets[i];
            }
        }

        private void Eat()
        {
            if (prey != null && Vector2.Distance(position, target) < 1)
            {
                Destroy(prey);
                spawner.rabbits.Remove(prey);
                creepScore++;
            }
        }

        protected override void Reproduce()
        {
            if (!_isAdult) return;
            if (creepScore < amountToGrow) return;

            for (int i = 0; i < Random.Range(0, _maxOffspring + 1); i++)
            {
                GameObject child = Instantiate(_child, (Vector2)position, Quaternion.identity, map.transform);
                child.GetComponent<AnimalBase>().spawner = spawner;
                child.GetComponent<AnimalBase>().map = map;
            }
            creepScore = 0;
        }
    }
}
