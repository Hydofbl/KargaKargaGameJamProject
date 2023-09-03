using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    // public GameObject DialogCanvas;
    [SerializeField] private GameObject DialogUI;
    [SerializeField] private TMP_Text npcNameTextField;
    [SerializeField] private TMP_Text DialogTextField;

    private DialogScriptableObject _dialog;

    private bool _dialogActive;

    private static DialogManager _instance;
    public static DialogManager Instance { get => _instance; }

    private void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _dialogActive = false;

        InputManager.Instance.OnPassButtonPressed += Instance_OnPassButtonPressed;
    }

    private void Instance_OnPassButtonPressed()
    {
        if (_dialogActive)
        {
            DialogTextField.text = _dialog.GetNextLine();
        }
    }

    public void StartDialog(DialogScriptableObject dialog)
    {
        InputManager.Instance.IsMovable = false;

        _dialog = dialog;

        npcNameTextField.text = _dialog.NPCName;
        ActivateDialogUI();

        DialogTextField.text = _dialog.GetNextLine();
    }

    public void ActivateDialogUI()
    {
        DialogUI.SetActive(true);
        _dialogActive = true;
    }

    public void DeactivateDialogUI()
    {
        DialogUI.SetActive(false);
        _dialogActive = false;

        InputManager.Instance.IsMovable = true;
    }
}
