using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] Mouse_RayCast Spawner;

    public void baton()
    {
        switch (this.gameObject.tag)
        {
            case "bunny":
                Spawner.Spawner = 1f;
                break;
            case "fox":
                Spawner.Spawner = 2f;
                break;
            case "bear":
                Spawner.Spawner = 3f;
                break;
            case "eagle":
                Spawner.Spawner = 4f;
                break;
            case "roman":
                Spawner.Spawner = 5f;
                break;
            case "rock":
                Spawner.Spawner = 6f;
                break;
            case "bigRock":
                Spawner.Spawner = 7f;
                break;
        }
    }
}
