using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using Shizounu.Library.Editor;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSandbox
{
    public class AnimalSandbox_RabbitManager : AnimalSandbox_AnimalManager, IReproduceable
    {
        [Header("Reproduction")]
        public int reproductionTurns = 3;
        [SerializeField,ReadOnly] private int turnCount;
        [SerializeField] private GameObject RabbitPrefab;
        
        public override void doTick(){
            base.doTick();
            turnCount++;
            if(canReproduce()){
                doReproduction();
            }
        }
        public bool canReproduce(){
            return (turnCount >= reproductionTurns);
        }
        public void doReproduction(){
            AnimalSandbox_GameManager.Instance.summonRabit(currentTile);
        }
    }
}
