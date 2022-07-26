using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

namespace Richie.TowerDefence
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void LoadAsync(int index)
        {
            StartCoroutine(LoadAsyncronously(index));
        }

        private IEnumerator LoadAsyncronously(int index)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                print(progress);
                yield return null;
            }
        }
    }
}
