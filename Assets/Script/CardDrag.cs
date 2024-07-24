using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IDragHandler, IDropHandler
{
    [SerializeField] GameObject _canvas;
    public void OnDrag(PointerEventData eventData)
    {
        transform.parent = _canvas.transform;
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
                transform.position = _canvas.transform.position;
                transform.parent = _canvas.transform;
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
