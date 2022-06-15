using UnityEngine.SceneManagement;
using UnityEngine;

namespace Richie.Platformer
{
    public class KillBox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<PlayerMovement>(out _))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
