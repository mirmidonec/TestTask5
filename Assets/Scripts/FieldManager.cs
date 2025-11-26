using UnityEngine;
using TMPro;
public class FieldManager : MonoBehaviour
{
    [SerializeField] private Cheems _cheems;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Cells[] _slots;
    [SerializeField] private GameObject _mem;
    private bool _allSlotsOccupied;
    private float _priceOfGen;
    public float PriceOfGen
    {
        get
        {
            return _priceOfGen;
        }
        set
        {
            _priceOfGen = value;
            _priceText.text = "Generate  (" + _priceOfGen.ToString() + ")";

        }
    }
    private void Awake()
    {
        PriceOfGen = 30;
    }
    public void Generate()
    {
        if (_cheems.CheemsF < PriceOfGen)
        {
            Debuger.Instance.DebugText.text = "Ne xvataet money(";
            return;
        }
        if (isFull())
        {
            Debuger.Instance.DebugText.text = "sloti zabiti(";
            return;

        }
        _cheems.CheemsF -= PriceOfGen;
        PriceOfGen += 5f;
        _allSlotsOccupied = false;
        for (int i = 0; i < _slots.Length; i++)
        {
            Cells cell = _slots[i];
            Memes mem = cell.GetComponentInChildren<Memes>();
            if (mem == null)
            {
                Instantiate(_mem, cell.transform);
                return;
            }
            else
            {
                _allSlotsOccupied = true;
            }
        }

        if (_allSlotsOccupied)
        {
            Debuger.Instance.DebugText.text = "All slots are full";
        }
    }
    private bool isFull()
    {
        foreach (Cells cell in _slots)
        {
            Memes mem = cell.GetComponentInChildren<Memes>();
            if (mem == null)
            {
                return false;
            }
        }
        return true;
    }
}