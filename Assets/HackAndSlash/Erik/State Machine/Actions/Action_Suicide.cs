using System.Collections;
using System.Collections.Generic;
using Shizounu.AI;
using UnityEngine;

namespace UnityIntro.Erik.HackAndSlash
{
    [CreateAssetMenu(fileName = "New SuicideAction", menuName = "Custom/AI/Action/Suicide")]
    public class Action_Suicide : Action
    {
        public override void Act(StateMachine stateMachine)
        {
            if(stateMachine.gameObject.TryGetComponent<HackAndSlash_Health>(out HackAndSlash_Health health)){
                health.Alive = false;
            }
        }
    }
}
