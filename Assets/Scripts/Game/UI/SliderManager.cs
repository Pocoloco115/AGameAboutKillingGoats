using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private string sliderKey;

    private ControlConfig config;
    private SliderBinding binding;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        config = ConfigManager.GetConfig();
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
            var binding = ConfigManager.GetConfig().sliders.Find(s => s.sliderName == sliderKey);
            if (binding != null) binding.value = v;

            ConfigManager.SaveConfig(ConfigManager.GetConfig());

            if(AudioManager.Instance != null)
            {
                AudioManager.Instance.ApplyVolumes();
            }
               
        });
    }
}
