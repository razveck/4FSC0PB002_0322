using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Richie.TowerDefence
{
    public class ChangeGradient : MonoBehaviour
    {
        [SerializeField] private bool isStoryActive = true;
        [SerializeField] private bool isEndlessActive = true;

        [Header("References")]
        [SerializeField] internal Button story;
        [SerializeField] internal Button endless;
        [SerializeField] private TMP_ColorGradient _color;
        [SerializeField] private TextMeshProUGUI[] _text;


        public void Active()
        {
            story.interactable = isStoryActive;
            endless.interactable = isEndlessActive;
        }

        public void ChangeColor()
        {
            foreach (var item in _text) item.colorGradientPreset = _color;
        }
    }
}
