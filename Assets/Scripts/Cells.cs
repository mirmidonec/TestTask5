using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System;

public class Cells : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject[] _combinedMemePrefabs;
    public void OnDrop(PointerEventData eventData)
    {
        Memes draggedMeme = eventData.pointerDrag.GetComponent<Memes>();
        if (transform.childCount == 0)
        {
            draggedMeme.ParentAfterDrag = transform;
        }
        else
        {
            Memes existingMeme = transform.GetChild(0).GetComponent<Memes>();

            if (existingMeme != null && existingMeme.name == draggedMeme.name)
            {
                int index = Array.IndexOf(_combinedMemePrefabs, _combinedMemePrefabs.FirstOrDefault(prefab => prefab.name + "(Clone)" == existingMeme.name));
                if (index < 0 || index + 1 > _combinedMemePrefabs.Length - 1)
                {
                    Debuger.Instance.DebugText.text = "zakonchilis' prefabi";
                    return;
                }
                Destroy(draggedMeme.gameObject);
                Destroy(existingMeme.gameObject);
                Instantiate(_combinedMemePrefabs[index + 1], transform).transform.SetParent(transform);
            }
        }

    }
}