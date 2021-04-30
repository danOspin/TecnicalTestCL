using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { Unselected, Approved, Rejected };

[Serializable]
public class Student
{
    
    public string studentName;
    public string studentLastName;
    public int studentAge;
    public string studentID;
    public string studentEmail;
    public float studentGrade;
    public Status status = Status.Unselected;
    Sprite studentSprite;


    public void DataInJsonToStudent(StudentJson studentfileData)
    {
        studentName = studentfileData.nombre;
        studentLastName = studentfileData.apellido;
        studentAge = studentfileData.edad;
        studentID = studentfileData.codigo;
        studentEmail = studentfileData.correo;
        studentGrade = studentfileData.nota;
        status = Status.Unselected;
    }

    public void SetSprite(Sprite sprite)
    {
        studentSprite = sprite;
    }

    public Sprite GetSprite()
    {
        return studentSprite;
    }

    public override string ToString()
    {
        return studentID;
    }

    public bool IsinWrongPlace()
    {
        if (studentGrade >= 3.0f && status != Status.Approved)
        {
            return true;
        }
        else if (studentGrade < 3.0f && status != Status.Rejected)
        {
            return true;
        }
        return false;
    }

}

[Serializable]
public class StudentJson
{

    public string nombre;
    public string apellido;
    public int edad;
    public string codigo;
    public string correo;
    public float nota;

}
[Serializable]
public class StudentsJson
{
    public StudentJson[] datos;
    
}

[Serializable]
public class Students
{
    public List<Student> datos;

}
