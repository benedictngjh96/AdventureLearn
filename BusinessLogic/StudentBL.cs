using Godot;
using System;
using System.Collections.Generic;

public class StudentBL : Node
{
    StudentDao studentDao = new StudentDao();
    CharSelectDao charSelectDao = new CharSelectDao();
    public bool CheckStudentExist()
    {
        bool exist = studentDao.CheckStudentExist(Global.studentId);
        return exist;
    } 
    public List<Character> GetCharacterList()
    {
        List<Character> characterList = charSelectDao.GetAllCharacters();
        return characterList;
    }
    public void InsertStudent(string studentId, string studentName, string inGameName, int charId)
    {
        studentDao.InsertStudent(studentId, studentName, inGameName, charId);
    }

}
