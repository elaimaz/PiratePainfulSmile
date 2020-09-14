using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private Slider durationSlider = null;
    [SerializeField]
    private Text durationValueText = null;

    [SerializeField]
    private Slider chaserSlider = null;
    [SerializeField]
    private Text chaserValueText = null;

    [SerializeField]
    private Slider shooterSlider = null;
    [SerializeField]
    private Text shooterValueText = null;

    [SerializeField]
    private GameObject playButton = null;
    [SerializeField]
    private GameObject optionsButton = null;
    [SerializeField]
    private GameObject buttonsMenus = null;


    private void Start()
    {
        durationSlider.value = Data.Duration;
        durationValueText.text = durationSlider.value.ToString() + "m";
        chaserSlider.value = Data.ChaserRate;
        chaserValueText.text = chaserSlider.value.ToString() + "s";
        shooterSlider.value = Data.ShooterRate;
        shooterValueText.text = shooterSlider.value.ToString() + "s";
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        playButton.SetActive(false);
        optionsButton.SetActive(false);
        buttonsMenus.SetActive(true);
    }

    public void CloseOptions()
    {
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        buttonsMenus.SetActive(false);
    }

    public void UpdateDurationSlider()
    {
        durationValueText.text = durationSlider.value.ToString() + "m";
        Data.Duration = Mathf.RoundToInt(durationSlider.value);
    }

    public void UpdateChaserSlider()
    {
        chaserValueText.text = chaserSlider.value.ToString() + "s";
        Data.ChaserRate = chaserSlider.value;
    }

    public void UpdateShooterSlider()
    {
        shooterValueText.text = shooterSlider.value.ToString() + "s";
        Data.ShooterRate = shooterSlider.value;
    }
}
