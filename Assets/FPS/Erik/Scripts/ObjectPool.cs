using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.FPS
{
    public class ObjectPool : MonoBehaviour
    {
        [Header("Initilization of the Pool")]
        public GameObject ObjectPrefab;
        public int PoolSize = 1024;

        public List<GameObject> FreePool;
        public List<GameObject> UsedPool;


        public void InitializePool()
        {
            GameObject curInit;
            for (int i = 0; i < PoolSize; i++)
            {
                curInit = Instantiate(ObjectPrefab, this.transform.position, new Quaternion());
                curInit.SetActive(false);
                curInit.transform.parent = this.gameObject.transform;
                FreePool.Add(curInit);
            }
        }


        public GameObject popElement()
        {
            GameObject element = FreePool[0];
            UsedPool.Add(element);
            FreePool.Remove(element);
            element.SetActive(true);

            return element;
        }

        public void returnElement(GameObject element)
        {
            UsedPool.Remove(element);
            FreePool.Add(element);
            element.SetActive(false);
            element.transform.position = this.transform.position;
        }



        private void Awake()
        {
            InitializePool();
        }
    }
}
