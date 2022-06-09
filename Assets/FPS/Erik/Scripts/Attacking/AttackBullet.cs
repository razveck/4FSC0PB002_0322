using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace UnityIntro.Erik.FPS
{
    public class AttackBullet : AttackBase
    {
        public ObjectPool bulletPool;

        public override void Attack()
        {
            GameObject bullet = bulletPool.popElement();
            bullet.transform.position = source.transform.position;
            bullet.transform.forward = source.transform.forward;
            bullet.GetComponent<Bullet>().source = bulletPool;
            bullet.GetComponent<Bullet>().damage = Damage;
            bullet.GetComponent<Bullet>().Initialize();
        }
        private void FixedUpdate() {
            if(InputManager.Instance.attackFlag){
                InputManager.Instance.attackFlag = false;
                Attack();
            }
        }
    }
}
