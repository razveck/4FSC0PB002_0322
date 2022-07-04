using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shizounu.AI;


namespace UnityIntro.Erik.AnimalSandbox
{
    public class AnimalSandbox_Action_RabbitReproduction : Action
    {
        public override void Act(StateMachine stateMachine)
        {
            AnimalSandbox_RabbitManager core = stateMachine.gameObject.GetComponent<AnimalSandbox_RabbitManager>();
            core.turnCount++;
            if(core.canReproduce()){
                core.doReproduction();
                core.turnCount = 0;
            }
        }
    }
}
