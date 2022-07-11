using UnityEngine.SceneManagement;
using UnityEngine;

public static class ReloadScene
{
    public static void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
