using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItem : MonoBehaviour
{
    public Student student;
    DragDropManager dragDropManager;
    public string type;
    public SpritesManager sManager;
    TextMeshProUGUI nameField;
    TextMeshProUGUI codeField;
    TextMeshProUGUI gradeField;
    Image sprite; 

    // Start is called before the first frame update
    void Start()
    {
        dragDropManager = GameObject.FindGameObjectWithTag("DragDropManager").GetComponent<DragDropManager>();
        sManager = GameObject.FindGameObjectWithTag("Sprites").GetComponent<SpritesManager>();
        nameField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "NameField").First();
        codeField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "CodeField").First();
        gradeField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "GradeField").First();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillData()
    {
        dragDropManager = GameObject.FindGameObjectWithTag("DragDropManager").GetComponent<DragDropManager>();
        nameField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "NameField").First();
        codeField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "CodeField").First();
        gradeField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "GradeField").First();
        sprite = GetComponent<Image>();
        nameField.text = student.studentName;
        codeField.text = student.studentID;
        gradeField.text = student.studentGrade.ToString();
        changeSprite();
    }

    void changeSprite()
    {

        sManager = GameObject.FindGameObjectWithTag("Sprites").GetComponent<SpritesManager>();
        Debug.Log(student.GetSprite().name);
        if (type == "neutral")
        {
            if (student.GetSprite().name.Contains("Boy"))
            {
                sprite.sprite = sManager.sprites_boy[0];
            }
            else
                sprite.sprite = sManager.sprites_girl[0];
            
        }
        else if (type == "approved")
        {
            if (student.GetSprite().name.Contains("Boy"))
            {
                sprite.sprite = sManager.sprites_boy[1];
            }
            else
                sprite.sprite = sManager.sprites_girl[1];
        }
        else if (type == "rejected")
        {
            if (student.GetSprite().name.Contains("Boy"))
            {
                sprite.sprite = sManager.sprites_boy[2];
            }
            else
                sprite.sprite = sManager.sprites_girl[2];
        }
        else
            sprite.sprite = student.GetSprite();



    }

    public void UpdateData()
    {
        if (type == "approved")
        {
            dragDropManager.approvalStudents.Remove(student);
            dragDropManager.neutralStudents.Add(student);
            dragDropManager.dragItem.enableDragAgain();
            dragDropManager.SingleNeutralZoneUpdate();
            Destroy(this.gameObject);
        }
        else
        {
            dragDropManager.rejectedStudents.Remove(student);
            dragDropManager.neutralStudents.Add(student);
            dragDropManager.dragItem.enableDragAgain();
            dragDropManager.SingleNeutralZoneUpdate();
            Destroy(this.gameObject);
        }

    }
}
