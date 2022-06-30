using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

using Random = UnityEngine.Random;
namespace UnityIntro.Erik.HackAndSlash
{
    public class HackAndSlash_WeightedRandomDrop : MonoBehaviour{
        [Serializable] public class RandomDrop{
            public GameObject Item;
            public float Weight = 1;
        }
        public List<RandomDrop> Drops;

        public GameObject getRandomDrop(){
            float fullWeight = 0;
            foreach (var item in Drops){
                fullWeight += item.Weight;
            }

            GameObject result = null;
            float val = Random.Range(0, fullWeight + 1);
            foreach (var item in Drops){
                if(item.Weight > val){
                    val -= item.Weight;
                } else {
                    result = item.Item;
                    break;
                }
            }
            return result;
        }
    }
}
