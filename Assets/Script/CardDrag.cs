using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardDrag : MonoBehaviour, IDragHandler, IDropHandler
{
    [SerializeField] GameObject _baceSlot;
    [SerializeField] CharacterData _characterData;
    public void OnDrag(PointerEventData eventData)
    {
        transform.parent = _baceSlot.transform;
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var hit in results)
        {
            if (hit.gameObject.name == "Set")
            {
                transform.position = hit.gameObject.transform.position;
                transform.parent = hit.gameObject.transform;
                Debug.Log("ƒZƒbƒg‚³‚ê‚½");
            }
            else
            {
                transform.position = _baceSlot.transform.position;
                transform.parent = _baceSlot.transform;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
