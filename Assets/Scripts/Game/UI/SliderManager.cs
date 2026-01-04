using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private string sliderKey = "Sensitivity";

    private ControlConfig config;
    private SliderBinding binding;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        config = ControlConfigManager.GetConfig();
        binding = config.sliders.Find(s => s.sliderName == sliderKey);

        if (binding == null)
        {
            binding = new SliderBinding { sliderName = sliderKey, value = 5f };
            config.sliders.Add(binding);
        }

        _slider.value = binding.value;
        _sliderText.text = binding.value.ToString("0");

        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = v.ToString("0");
            var binding = ControlConfigManager.GetConfig().sliders.Find(s => s.sliderName == sliderKey);
            if (binding != null) binding.value = v;

            ControlConfigManager.SaveConfig(ControlConfigManager.GetConfig());
        });
    }
}
