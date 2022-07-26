using UnityEngine;

namespace Richie.TowerDefence
{
    public class Quit : MonoBehaviour
    {
        public void QuitApp()
        {
            #if UNITY_STANDALONE
            Application.Quit();
            #endif

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
