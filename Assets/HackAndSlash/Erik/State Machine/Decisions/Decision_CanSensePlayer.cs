using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Shizounu.AI;
using JetBrains.Annotations;

namespace UnityIntro.Erik.HackAndSlash
{
    [CreateAssetMenu(fileName = "New CanSensePlayer", menuName = "Custom/AI/Decision/Can Sense Player")]
    public class Decision_CanSensePlayer : Decision
    {
        public LayerMask playerLayer;
        public float SenseRange = 5f;
        public override bool Decide(StateMachine stateMachine){
            Collider[] hits = Physics.OverlapSphere(stateMachine.transform.position, SenseRange, playerLayer);
            return hits.Length >= 1;
        }
    }
}
