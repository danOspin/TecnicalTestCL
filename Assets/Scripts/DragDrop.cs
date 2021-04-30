using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public GameObject neutralZone;
    public Student student;
    public Sprite nullStudent;

    TextMeshProUGUI nameField;
    TextMeshProUGUI codeField;
    TextMeshProUGUI ageField;
    TextMeshProUGUI emailField;
    TextMeshProUGUI gradeField;
    public Image spriteStudent;
    public bool enableDrag;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        enableDrag = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (enableDrag)
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (enableDrag)
             rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (enableDrag)
        {

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = neutralZone.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void enableDragAgain()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        enableDrag = true;
    }
    public void FillComponents()
    {
        nameField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "NameField").First();
        codeField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "CodeField").First();
        ageField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "AgeField").First();
        emailField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "EmailField").First();
        gradeField = GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.tag == "GradeField").First();
    }

    public void UpdateNeutralZone(Student stu)
    {
        if (stu != null)
        {
            FillComponents();
            student = stu;
            nameField.text = stu.studentName + " " + stu.studentLastName;
            codeField.text = EmptyString(stu.studentID);
            ageField.text = stu.studentAge.ToString();
            emailField.text = EmptyString(stu.studentEmail);
            gradeField.text = stu.studentGrade.ToString();
            gradeField.color = SeverityGrade(stu.studentGrade);
            spriteStudent.sprite = stu.GetSprite();
        }
        else
        {
            student = null;
            enableDrag = false;
            nameField.text = "No registra";
            codeField.text = "No registra";
            ageField.text = "No registra";
            emailField.text = "No registra";
            gradeField.text = "0";
            gradeField.color = SeverityGrade(0);
            spriteStudent.sprite = nullStudent;
            canvasGroup.blocksRaycasts = false;
        }

    }

    string EmptyString(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return "No registra";
        }
        else
            return data;
    }

    Color SeverityGrade(float grade)
    {
        if (grade >= 3.0f)
        {
            return Color.green;
        }
        else
            return Color.red;
    }
}
