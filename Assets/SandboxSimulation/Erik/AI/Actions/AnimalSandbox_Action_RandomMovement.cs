using System;
using System.Collections;
using System.Collections.Generic;
using Shizounu.AI;
using UnityEngine;

using Random = UnityEngine.Random;
namespace UnityIntro.Erik.AnimalSandbox
{
    [CreateAssetMenu(menuName = "Animal Sandbox/Actions/Random Movement", fileName = "new RandomMovement")]
    public class AnimalSandbox_Action_RandomMovement : Shizounu.AI.Action
    {
        public LayerMask tileLayer;
        public float searchRange = 30;
        public override void Act(StateMachine stateMachine){
            Collider[] colliders = Physics.OverlapSphere(stateMachine.transform.position, searchRange, tileLayer);
            List<AnimalSandbox_TileManager> tiles = new();
            foreach (var item in colliders){
                tiles.Add(item.gameObject.GetComponent<AnimalSandbox_TileManager>());
            }

            stateMachine.GetComponent<AnimalSandbox_AnimalManager>().doMovement(tiles[Mathf.RoundToInt(Random.Range(0, tiles.Count))]);
        }
    }
}
