using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using Shizounu.Library.Editor;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSandbox
{
    public class AnimalSandbox_RabbitManager : AnimalSandbox_AnimalManager, IReproduceable
    {
        [Header("Reproduction")]
        public int reproductionTurns = 3;
        public int ReproductionAmount = 5;
        [ReadOnly] public int turnCount;
        [SerializeField] private GameObject RabbitPrefab;
        
        public override void doTick(){
            base.doTick();
        }
        public bool canReproduce(){
            return (turnCount >= reproductionTurns);
        }
        public void doReproduction(){
            for (int i = 0; i < ReproductionAmount; i++)
                AnimalSandbox_GameManager.Instance.summonAnimal(AnimalSandbox_GameManager.Instance.Rabbit,currentTile);
        }
    }
}
