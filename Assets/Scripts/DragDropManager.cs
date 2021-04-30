using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragDropManager : MonoBehaviour
{

    public List<Student> students;
    public List<Student> neutralStudents;
    public List<Student> approvalStudents;
    public List<Student> rejectedStudents;
    public DragDrop dragItem;

    public TextMeshProUGUI finalMessage;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {

        //neutralStudents = new List<Student>();
        approvalStudents = new List<Student>();
        rejectedStudents = new List<Student>();
        //students = new List<Student>();
    }
   

    public void updateList(List<Student> studentList)
    {
        students = studentList;
        neutralStudents = new List<Student>(studentList);
        dragItem.UpdateNeutralZone(neutralStudents.First());
    }

   public void SingleNeutralZoneUpdate()
    {
        if (neutralStudents.Count()==0)
        {
            dragItem.UpdateNeutralZone(null);
        }
        else
        {
            dragItem.UpdateNeutralZone(neutralStudents.First());
        }
    }

    public void ValidationSecondScreen()
    {
        panel.SetActive(true);
        finalMessage.text = "";

        int mistakesOnApproval = approvalStudents.Where(x => x.studentGrade < 3.0f || x.status != Status.Approved).Count();
        int mistakesOnRejected = rejectedStudents.Where(x => x.studentGrade > 3.0f || x.status != Status.Rejected).Count();


        if (mistakesOnApproval>0)
        {
            finalMessage.text = "Tras la segunda revision, se encuentran: "+ mistakesOnApproval + " inconsistencias en el grupo de estudiantes aprobados.\n"; 

        }
        if (mistakesOnRejected > 0)
        {
            finalMessage.text += "Uy Profe, Subame la nota. \nTras la segunda revision, se encuentran: "+ mistakesOnRejected + " inconsistencias en el grupo de estudiantes rechazados.";
        }

        if (mistakesOnApproval == 0 && mistakesOnRejected ==0 && neutralStudents.Count()==0)
        {
            finalMessage.text = "Hora de tomar cafe. \nLos estudiantes han sido evaluados correctamente.";
        }
        else if (neutralStudents.Count() > 0)
        {
            finalMessage.text = "Aun faltan estudiantes por revisar.";
        }

        


    }

}
