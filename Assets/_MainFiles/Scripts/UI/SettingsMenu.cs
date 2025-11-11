using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] PlayerSettingsSO playerSettingsToShare;
    [SerializeField] PlayerSettingsSO playerSettingsStandart;
    [SerializeField] TMP_InputField bpm;
    [SerializeField] TMP_InputField moveSpeed;
    [SerializeField] TMP_InputField sideSpeed;
    [SerializeField] TMP_InputField jumpDuration;
    [SerializeField] TMP_InputField jumpOffset;
    [SerializeField] TMP_InputField slideDuration;
    [SerializeField] TMP_InputField slideOffset;

    void Start()
    {
        GameHandler.Instance.OnBuffGet += UpdatePlayerStatsText;

        PlayerSettingsSO playerSettings = GameHandler.Instance.GetPlayerSettings();
        SetUITextValues(playerSettings);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UpdatePlayerSettings();
        }
    }

    void SetUITextValues(PlayerSettingsSO playerSettings)
    {
        bpm.text = "" + playerSettings.stepsPerMinute;
        moveSpeed.text = "" + playerSettings.runSpeed;
        sideSpeed.text = "" + playerSettings.strafeSpeed;
        jumpDuration.text = "" + playerSettings.jumpDuration;
        jumpOffset.text = "" + playerSettings.jumpOffset;
        slideDuration.text = "" + playerSettings.slideDuration;
        slideOffset.text = "" + playerSettings.slideOffset;
    }

    public void UpdatePlayerSettings()
    {
        // playerSettingsToShare.runSpeed = moveSpeed;
        float.TryParse(bpm.text, out playerSettingsToShare.stepsPerMinute);
        float.TryParse(moveSpeed.text, out playerSettingsToShare.runSpeed);
        float.TryParse(sideSpeed.text, out playerSettingsToShare.strafeSpeed);
        float.TryParse(jumpDuration.text, out playerSettingsToShare.jumpDuration);
        float.TryParse(jumpOffset.text, out playerSettingsToShare.jumpOffset);
        float.TryParse(slideDuration.text, out playerSettingsToShare.slideDuration);
        float.TryParse(slideOffset.text, out playerSettingsToShare.slideOffset);

        GameHandler.Instance.OnMovementStatsUpdate?.Invoke(playerSettingsToShare);

        // Debug.Log("updateSettingsFromUI");
    }

    void UpdatePlayerStatsText(BuffStatsSO buff)
    {
        bpm.text = "" + GameHandler.Instance.bpm;
        moveSpeed.text = "" + GameHandler.Instance.runSpeed;
        sideSpeed.text = "" + GameHandler.Instance.strafeSpeed;
        jumpDuration.text = "" + GameHandler.Instance.jumpDuration;
        jumpOffset.text = "" + GameHandler.Instance.jumpOffset;
        slideDuration.text = "" + GameHandler.Instance.slideDuration;
        slideOffset.text = "" + GameHandler.Instance.slideOffset;
    }

    public void SetStandartPlayerSettings()
    {
        GameHandler.Instance.OnMovementStatsUpdate?.Invoke(playerSettingsStandart);
        SetUITextValues(playerSettingsStandart);

        // Debug.Log("updateSettingsToStandart");
    }

    public void RestartScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

}
