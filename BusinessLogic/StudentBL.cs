using Godot;
using System;
using System.Collections.Generic;

public class StudentBL : Node
{

    /// <summary>
    /// Return true if StudentId already exists
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public bool CheckStudentExist(int studentId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckStudentExist(studentId);
        return exist;
    }
    public bool CheckStudentCharExist(int studentId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckStudentExist(studentId);
        return exist;
    }
    public bool CheckGoogleCharExist(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckGoogleCharExist(googleId);
        return exist;
    }
    public bool CheckFacebookCharExist(string fbId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckFacebookCharExist(fbId);
        return exist;
    }
    public bool CheckGoogleExist(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckGoogleExist(googleId);
        return exist;
    }
    public bool CheckFacebookExist(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckFacebookExist(googleId);
        return exist;
    }
    /// <summary>
    /// Return int value 1 if userName and password is valid
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public int CheckStudentLogin(string userName, string password)
    {
        return 1;
    }
    /// <summary>
    /// Return int result 1 if InsertStudent has executed successful
    /// </summary>
    /// <param name="studentName"></param>
    /// <param name="charId"></param>
    /// <param name="studentEmail"></param>
    /// <param name="studentUsername"></param>
    /// <param name="studentPassword"></param>
    /// <returns></returns>
    public int InsertGoogleStudent(string studentName, string studentEmail, string googleId)
    {
        StudentDao studentDao = new StudentDao();
        int result = studentDao.InsertGoogleStudent(studentName, studentEmail, googleId);
        return result;
    }
    public int InsertFacebookStudent(string studentName, string studentEmail, string fbId)
    {
        StudentDao studentDao = new StudentDao();
        int result = studentDao.InsertFacebookStudent(studentName, studentEmail, fbId);
        return result;
    }
    public int GetGoogleStudentId(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        return studentDao.GetGoogleStudent(googleId).StudentId;
    }
    public int GetFacebookStudentId(string fbId)
    {
        StudentDao studentDao = new StudentDao();
        return studentDao.GetFacebookStudent(fbId).StudentId;
    }
    public int UpdateStudentCharacter(int charId, int studentId){
        StudentDao studentDao = new StudentDao();
        return studentDao.UpdateStudentCharacter(charId, studentId);
    }
    public Student GetStudentCharacter(int studentId)
    {
        StudentDao studentDao = new StudentDao();
        return studentDao.GetStudentCharacter(studentId);
    }


}
