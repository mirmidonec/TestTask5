using UnityEngine;
using TMPro;

public class Debuger : MonoBehaviour
{
    public static Debuger Instance { get; private set; }

    [HideInInspector] public TMP_Text DebugText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DebugText = GetComponent<TMP_Text>();
        DebugText.text = "";
    }
}
