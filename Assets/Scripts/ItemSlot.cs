using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    GameObject neutralZone;
    public string nameZone;
    DragDropManager dragDropManager;
    public GameObject prefab;
    public GameObject child;

    void Start()
    {
        neutralZone = GameObject.FindGameObjectWithTag("NeutralZone");
        dragDropManager = GameObject.FindGameObjectWithTag("DragDropManager").GetComponent<DragDropManager>();
    }

    

    public void OnDrop(PointerEventData eventData)
    {
        Student studentToUpdate = eventData.pointerDrag.GetComponent<DragDrop>().student;
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = neutralZone.GetComponent<RectTransform>().anchoredPosition;

            //Se valida que sea diferente a Neutral para que no reduzca la lista de neutrales por error.
            if (nameZone == "approved")
            {
                dragDropManager.approvalStudents.Add(studentToUpdate);
                dragDropManager.neutralStudents.Remove(studentToUpdate);
                var approvedPrefab = Instantiate(prefab, child.transform);
                approvedPrefab.GetComponent<ScrollItem>().type = "approved";
                approvedPrefab.GetComponent<ScrollItem>().student = studentToUpdate;
                approvedPrefab.GetComponent<ScrollItem>().FillData();
                dragDropManager.SingleNeutralZoneUpdate();

                //Se actualiza lista de neutrales y aprobados del manager. y se cambia sprite.
            }
            else if (nameZone == "rejected")
            {
                dragDropManager.rejectedStudents.Add(studentToUpdate);
                dragDropManager.neutralStudents.Remove(studentToUpdate);
                var approvedPrefab = Instantiate(prefab, child.transform);
                approvedPrefab.GetComponent<ScrollItem>().type = "rejected";
                approvedPrefab.GetComponent<ScrollItem>().student = studentToUpdate;
                approvedPrefab.GetComponent<ScrollItem>().FillData();
                dragDropManager.SingleNeutralZoneUpdate();
            }
        }
    }

}