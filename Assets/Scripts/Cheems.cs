using UnityEngine;
using TMPro;
using System;

public class Cheems : MonoBehaviour
{
    [SerializeField] private TMP_Text _cheemsText;
    private float _cheems;
    public float CheemsF
    {
        get
        {
            return _cheems;
        }
        set
        {
            _cheems = value;
            _cheemsText.text = "Cheems: " + MathF.Round(_cheems,0);
        }
    }

    private void Awake()
    {
        CheemsF = 100f;
    }
}