using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoustingUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_InputField joinCodeField;
    [SerializeField] private GameObject networkManager;


    private void Awake()
    {
        RelayManager relayManager = networkManager.GetComponent<RelayManager>();

        hostButton.onClick.AddListener(async () =>
        {
            string joinCode = await relayManager.CreateRelay();
            joinCodeField.text = joinCode;
            joinCodeField.DeactivateInputField();
        });

        joinButton.onClick.AddListener(() =>
        {
            string joinCode = joinCodeField.text;
            relayManager.JoinRelay(joinCode);
        });
    }
}
