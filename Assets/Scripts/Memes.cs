using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Memes : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Conditions")]
    public TMP_Text LvlText;
    public TMP_Text SpeedText;
    private Cheems _cheems;
    private Image _memIcon;
    [SerializeField] private GameObject _moverPrefabs;
    private GameObject _oneThatChosen;
    [HideInInspector] public Transform ParentAfterDrag;
    [SerializeField] private float _income;
     [SerializeField] private float _lvl;


    private void Awake()
    {
        _cheems = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Cheems>();
        _memIcon = GetComponent<Image>();
        SpeedText.text = _income + "/s";
        LvlText.text = _lvl.ToString();
    }
    private void Start()
    {
        _oneThatChosen = _moverPrefabs;
        if (_oneThatChosen != null)
        {
            Vector3 randomPosition = GetRandomPosition();
            _oneThatChosen = Instantiate(_oneThatChosen, randomPosition, Quaternion.identity);
        }
          StartCoroutine(GenerateCoins());
    }
    private Vector3 GetRandomPosition()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float objectWidth = _oneThatChosen.GetComponent<Renderer>().bounds.size.x;
        float objectHeight = _oneThatChosen.GetComponent<Renderer>().bounds.size.y;

        float randomX = Random.Range(-cameraWidth / 2 + objectWidth / 2, cameraWidth / 2 - objectWidth / 2);
        float randomY = Random.Range(-cameraHeight / 2 + objectHeight / 2, cameraHeight / 2 - objectHeight / 2);

        return new Vector3(randomX, randomY, 0);
    }

    private IEnumerator GenerateCoins()
    {
        while (true)
        {
            _cheems.CheemsF += _income;
            yield return new WaitForSeconds(1f);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _memIcon.raycastTarget = false;
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)transform.parent,
            Input.mousePosition,
            eventData.pressEventCamera,
            out worldPoint);
        transform.position = worldPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _memIcon.raycastTarget = true;
        transform.SetParent(ParentAfterDrag);
    }
    private void OnDestroy()
    {
        Destroy(_oneThatChosen);
    }
}