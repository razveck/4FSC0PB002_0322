using UnityEngine;
using Vectors_Richie;

namespace Richie.Platformer
{
    public class ButtonInteraction : MonoBehaviour
    {

        Vector _interaction = new();
        public float Speed, maxTime = 1f;
        public bool _switch;

        public float timer;

        public GameObject _stones;

        private void Start()
        {
            _interaction = transform.position;
            _stones.SetActive(false);
        }


        private void Update()
        {
                
            timer -= Time.deltaTime;

            if (!_switch && transform.position.y < _interaction.y && timer <= 0f)
            {
                _stones.SetActive(false);
                transform.position = new Vector(transform.position.x, transform.position.y + Speed * Time.deltaTime, transform.position.z);
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.transform.TryGetComponent<Rigidbody>(out _))
            {
                if (!_switch)
                    _switch = true;

                transform.position = new Vector(transform.position.x, transform.position.y - Speed * Time.deltaTime, transform.position.z);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.transform.TryGetComponent<Rigidbody>(out _))
            {
                if (!_switch)
                    return;

                _stones.SetActive(true);

                timer = maxTime;
                _switch = false;
            }
        }


    }
}
