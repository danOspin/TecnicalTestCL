using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    public List<Student> studentsList = new List<Student>();
    public GameObject nameUI;
    public GameObject lastNameUI;
    public GameObject ageUI;
    public GameObject emailUI;
    public GameObject gradeUI;
    public Image spriteUI;

    public GameObject container;
    public GameObject prefab;
    public string jsonFileName;
    public DragDropManager ddm;
    public GameObject message;

    public GameObject interfaz1;

    SpritesManager spriteManager;
    
    List<Student> studentBlackList = new List<Student>();
    StudentsJson studentsInJson;

    List<Student> wrongList;
    bool alreadyActive=false;

    // Start is called before the first frame update
    void Start()
    {
        spriteManager = GameObject.FindGameObjectWithTag("Sprites").GetComponent<SpritesManager>();
        ReachList();
        //UpdateUI(studentsList.First());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void UpdateSLToDDManager()
    {
        if (!alreadyActive)
        {
            ddm.updateList(studentsList);
            alreadyActive = true;
        }
    }

    //This function converts the data readed from JSON file and converts to a list of students and fills the information
    //related to the scrollview and creates a black list that helps to identify students with incorrect data.
    void ReachList()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            studentsInJson = JsonUtility.FromJson<StudentsJson>(dataAsJson);

            foreach (StudentJson studentInFile in studentsInJson.datos)
            {
                Student student = new Student();
                student.DataInJsonToStudent(studentInFile);
                student.SetSprite(spriteManager.RandomSprite());
                studentsList.Add(student);
                DuplicatePrefab(container,prefab,student);
                if (student.studentGrade==0 || string.IsNullOrWhiteSpace(student.studentID))
                {
                    studentBlackList.Add(student);
                }
            }

        }
    }


    public void DuplicatePrefab(GameObject container,GameObject prefab, Student student)
    {

        var ItemField = Instantiate(prefab, container.transform);
        ItemField.transform.parent = container.transform;
        ItemField.name = "Student_" + student.studentID + " " + student.studentName;
        ItemField.GetComponent<ItemManager>().sceneManager = this;
        ItemField.GetComponent<ItemManager>().FillComponents();
        ItemField.GetComponent<ItemManager>().student = student;
        ItemField.GetComponent<ItemManager>().FillStudentData();


    }

    public void UpdateUI(Student student)
    {

        nameUI.GetComponent<TextMeshProUGUI>().text = student.studentName;
        lastNameUI.GetComponent<TextMeshProUGUI>().text = student.studentLastName;
        ageUI.GetComponent<TextMeshProUGUI>().text = student.studentAge.ToString();
        emailUI.GetComponent<TextMeshProUGUI>().text = student.studentEmail;
        gradeUI.GetComponent<TextMeshProUGUI>().text = student.studentGrade.ToString();
        gradeUI.GetComponent<TextMeshProUGUI>().color = SeverityGrade(student.studentGrade);
        spriteUI.sprite = student.GetSprite();
        Debug.Log(spriteUI.sprite.name);
        Debug.Log(spriteUI.sprite.texture.name);
        spriteUI.color = new Color(spriteUI.color.r, spriteUI.color.g, spriteUI.color.b, 1);

    }


    public void NextScreen()
    {
        if (wrongList.Count()>0)
        {
            message.SetActive(false);
        }
        else
        {
            UpdateSLToDDManager();
            message.SetActive(false);
            interfaz1.SetActive(false);
            ddm.gameObject.SetActive(true);
            //pasa info a screen 2;
        }
    }


    public void ValidateTable()
    {
        wrongList = new List<Student>();
        foreach(Student s in studentsList)
        {
            if (s.IsinWrongPlace())
            {
                wrongList.Add(s);
            }
        }
        if (wrongList.Count()>0)
        {
            message.GetComponentInChildren<TextMeshProUGUI>().text = "Revisar la tabla nuevamente. \nActualmente hay "+ wrongList.Count() +" estudiantes con las casillas incorrectas";
            

        }
        else
        {
            message.GetComponentInChildren<TextMeshProUGUI>().text = "Los estudiantes están en con las opciones correctas. Puede continuar.";
        }

        if (studentBlackList.Count() > 0)
        {
            message.GetComponentInChildren<TextMeshProUGUI>().text += "\n\nPor favor, revisar el archivo de origen. Hay unos estudiantes con datos incompletos.";
        }
        message.gameObject.SetActive(true);

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