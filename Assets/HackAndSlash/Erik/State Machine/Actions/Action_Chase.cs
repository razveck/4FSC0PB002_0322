using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shizounu.AI;

namespace UnityIntro.Erik.HackAndSlash
{
    [CreateAssetMenu(fileName = "New ChaseAction", menuName = "Custom/AI/Action/Chase")]
    public class Action_Chase : Action
    {
        public override void Act(StateMachine stateMachine){
            stateMachine.navMeshAgent.CalculatePath(FindObjectOfType<HackAndSlash_PlayerController>().transform.position, stateMachine.path);
            stateMachine.navMeshAgent.SetPath(stateMachine.path);
        } 
        
    }
}
