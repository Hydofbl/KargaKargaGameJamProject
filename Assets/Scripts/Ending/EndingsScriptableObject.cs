using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ending Object", menuName = "Ending")]
public class EndingsScriptableObject : ScriptableObject
{
    [SerializeField]
    [TextArea]
    public string EndingName;

    [SerializeField]
    [TextArea]
    public string EndingText;

    public Sprite EndingSprite;
}
