using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private string sliderKey = "Sensitivity"; 

    void Start()
    {
        ControlConfig config = ControlConfigManager.LoadConfig();
        SliderBinding binding = config.sliders.Find(s => s.sliderName == sliderKey);

        float savedValue = binding != null ? binding.value : 5f;
        _slider.value = savedValue;
        _sliderText.text = savedValue.ToString("0");

        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = v.ToString("0");

            binding = config.sliders.Find(s => s.sliderName == sliderKey);
            if (binding != null)
                binding.value = v;
            else
                config.sliders.Add(new SliderBinding { sliderName = sliderKey, value = v });

            ControlConfigManager.SaveConfig(config);
        });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
