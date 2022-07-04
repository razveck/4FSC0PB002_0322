using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSandbox
{
    public interface IReproduceable
    {
        public void doTick();
        public bool canReproduce();
        public void doReproduction();
    }
}
