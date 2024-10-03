using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardDrag : MonoBehaviour, IDragHandler, IDropHandler
{
    [SerializeField] Canvas canvas;
    [Space(5),Header("�f�b�L���Ɏc��L�����N�^�[�̃X���b�g")]
    [SerializeField] GameObject _baceSlot;

    //�h���b�O��
    public void OnDrag(PointerEventData eventData)
    {
        canvas = GetComponentInParent<Canvas>();
        transform.parent = null;
        transform.SetParent(canvas.transform);
        transform.position = eventData.position;
    }

    //�h���b�v��
    public void OnDrop(PointerEventData eventData)
    {
        //�h���b�v�悪�����ƈ�v���Ȃ���΃f�b�L���̃X���b�g�ɖ߂����
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var hit in results)
        {
            if (hit.gameObject.name == "Set")
            {
                transform.position = hit.gameObject.transform.position;
                transform.parent = hit.gameObject.transform;
                Debug.Log("�Z�b�g���ꂽ");
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
