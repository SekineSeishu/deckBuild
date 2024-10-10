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
    [Space(5),Header("デッキ内に残るキャラクターのスロット")]
    [SerializeField] CardBase _characterBace;


    //ドラッグ時
    public void OnDrag(PointerEventData eventData)
    {
        canvas = GetComponentInParent<Canvas>();
        transform.parent = null;
        transform.SetParent(canvas.transform);
        transform.position = eventData.position;
    }

    //ドロップ時
    public void OnDrop(PointerEventData eventData)
    {
        //ドロップ先が条件と一致しなければデッキ内のスロットに戻される
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var hit in results)
        {
            if (hit.gameObject.name == "Set")
            {
                //親オブジェを変更しキャラクターをセット判定にする
                MemberSlot memberSlot = hit.gameObject.GetComponent<MemberSlot>();
                if (memberSlot.onMenber)
                {
                    memberSlot.gameObject.GetComponentInChildren<CardDrag>().backSlot();
                }
                transform.position = hit.gameObject.transform.position;
                transform.parent = hit.gameObject.transform;
                memberSlot.menberChack();
                _characterBace._data._set = true;
                Debug.Log("セットされた");
            }
            else
            {
                backSlot();
            }
        }
        _characterBace.setCharacter();
    }

    private void backSlot()
    {
        //元の親オブジェに戻りセット判定を外す
        transform.position = _characterBace.transform.position;
        transform.parent = _characterBace.transform;
        _characterBace._data._set = false;
    }
}
