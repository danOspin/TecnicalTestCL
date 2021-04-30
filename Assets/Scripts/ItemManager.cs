using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Student student;
    public SceneManager sceneManager;
    TextMeshProUGUI nameCell;
    TextMeshProUGUI codeCell;
    [SerializeField]
    Image approvedSprite;
    [SerializeField]
    Image rejectedSprite;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {

    }
    public void FillComponents()
    {
        nameCell = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "NameField").First();
        codeCell = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "CodeField").First();

    }
    public void FillStudentData()
    {
        nameCell.text = student.studentName + " " + student.studentLastName;
        codeCell.text = student.studentID;
    }

    

    public void ShowInfoPanel()
    {
        sceneManager.UpdateUI(student);
    }


    public void ApprovedClicked()
    {
        if (!student.status.Equals(Status.Approved))
        {
                rejectedSprite.color = TransparencyColor(rejectedSprite.color);
                approvedSprite.color = OpaqueColor(approvedSprite.color);
                student.status = Status.Approved;
                sceneManager.UpdateUI(student);
       
        }
    }

  

    public void RejectedClicked()
    {
        if (!student.status.Equals(Status.Rejected))
        {
            rejectedSprite.color = OpaqueColor(rejectedSprite.color);
            approvedSprite.color = TransparencyColor(approvedSprite.color);
            student.status = Status.Rejected;
            sceneManager.UpdateUI(student);
         
        }
    }


    public Color OpaqueColor(Color c)
    {
        return new Color(c.r, c.g, c.b, 1);
    }
    public Color TransparencyColor(Color c)
    {
        return new Color(c.r, c.g, c.b, 0.3f);
    }

}
