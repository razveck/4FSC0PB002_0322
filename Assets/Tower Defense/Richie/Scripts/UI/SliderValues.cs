using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Richie.TowerDefence
{
    public class SliderValues : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _display;

        public void Slider()
        {
            CameraController.ZoomModifier = _slider.value;
            _display.text = $"{_slider.value}";
        }

        public void InputField()
        {
            if (int.TryParse(_display.text, out int value))
            {
                CameraController.ZoomModifier = value;
                _slider.value = value;
            }
        }
    }
}
