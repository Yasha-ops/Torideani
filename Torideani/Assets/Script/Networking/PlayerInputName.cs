using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputName : MonoBehaviour
{
    [SerializeField] private InputField nameInputField = null;
    [SerializeField] private Button continueButton = null; //test

    private const string PlayerPrefsNameKey = "PlayerName";

    private void Start()
    {
        SetUpInputField(); 
    }

    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

        nameInputField.text = defaultName;
        
        Debug.Log(defaultName);

        SetPlayerName(defaultName); 
    }

    public void SetPlayerName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            continueButton.interactable = true; 
        }
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;
        PhotonNetwork.NickName = playerName; 
        PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
    }
}
