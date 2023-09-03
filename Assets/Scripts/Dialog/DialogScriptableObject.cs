using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Object", menuName = "Dialog")]
public class DialogScriptableObject : ScriptableObject
{
    public string NPCName;

    [SerializeField]
    [TextArea]
    private List<string> DialogLines;

    public int LineIndex = 0;

    private void OnEnable()
    {
        LineIndex = 0;
    }

    public string GetNextLine()
    {
        if(LineIndex < DialogLines.Count)
        {
            return DialogLines[LineIndex++];
        }

        OnDialogComplete();
        return DialogLines[LineIndex-1];
    }

    public string GetNPCName()
    {
        return NPCName;
    }

    private void OnDialogComplete()
    {
        DialogManager.Instance.DeactivateDialogUI();

        LineIndex--;
    }
}
