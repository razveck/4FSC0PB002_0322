using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : HealthBase
{

    protected override float MaxHealth()
    {

        return health;
    }

    protected override void OnDeath()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
