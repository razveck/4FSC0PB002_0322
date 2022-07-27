using System.Collections.Generic;
using UnityEngine;

namespace Richie
{
    public class EnemyHealth : HealthBase
    {
        internal Transform container;
        internal PlayerBag playerBag;


		[SerializeField]
		private ParticleSystem _hitParticles = default;

		protected override void Start() {
			OnHit += EnemyHealth_OnHit;
			base.Start();
		}

		private void EnemyHealth_OnHit(int value) {
			_hitParticles.Play();
            var shape = _hitParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Box;
            
		}

		protected override void OnDeath()
        {
            GiveLoot();
            base.OnDeath();
        }

        private void GiveLoot()
        {
            int num = 0;
            List<GameObject> loot = lootTable.GetLoot();
            Vector2[] directions = new Vector2[] { Vector2.zero, Vector2.up, Vector2.down, Vector2.left, Vector2.right };

            for (int i = 0; i < loot.Count; i++)
            {
                if (num >= directions.Length) num = 0;
                GameObject item = Instantiate(loot[i], transform.position + (Vector3)directions[num], Quaternion.identity, container);
                item.GetComponent<AutoCollect>().target = GetComponent<EnemyMove>().Target;
                item.GetComponent<AutoCollect>().playerBag = playerBag;
                num++;
            }
        }
    }
}
