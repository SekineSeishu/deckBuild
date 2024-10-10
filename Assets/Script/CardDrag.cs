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
    [SerializeField] CardBase _characterBace;


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
                //�e�I�u�W�F��ύX���L�����N�^�[���Z�b�g����ɂ���
                MemberSlot memberSlot = hit.gameObject.GetComponent<MemberSlot>();
                if (memberSlot.onMenber)
                {
                    memberSlot.gameObject.GetComponentInChildren<CardDrag>().backSlot();
                }
                transform.position = hit.gameObject.transform.position;
                transform.parent = hit.gameObject.transform;
                memberSlot.menberChack();
                _characterBace._data._set = true;
                Debug.Log("�Z�b�g���ꂽ");
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
        //���̐e�I�u�W�F�ɖ߂�Z�b�g������O��
        transform.position = _characterBace.transform.position;
        transform.parent = _characterBace.transform;
        _characterBace._data._set = false;
    }
}
