using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for Student
/// </summary>
public class StudentBL : Node
{

    /// <summary>
    /// Check if Student has an existing account
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return true if Student's record exist</returns>
    public bool CheckStudentExist(int studentId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckStudentExist(studentId);
        return exist;
    }
    /// <summary>
    /// Check if Student has an existing account
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return true if Student's record exist</returns>
    public bool CheckStudentCharExist(int studentId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckStudentExist(studentId);
        return exist;
    }
    /// <summary>
    /// Check if Student who has logged in with Google account has an existing Character
    /// </summary>
    /// <param name="googleId"></param>
    /// <returns>Return true if Student has existing Character</returns>
    public bool CheckGoogleCharExist(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckGoogleCharExist(googleId);
        return exist;
    }
    /// <summary>
    /// Check if Student who has logged in with Facebook account has an existing Chrracter
    /// </summary>
    /// <param name="fbId"></param>
    /// <returns>Return true if Student has existing Character</returns>
    public bool CheckFacebookCharExist(string fbId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckFacebookCharExist(fbId);
        return exist;
    }
    /// <summary>
    /// Check if there is an existing Student's Google account
    /// </summary>
    /// <param name="googleId"></param>
    /// <returns>Return true if Student's Google account exists</returns>
    public bool CheckGoogleExist(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckGoogleExist(googleId);
        return exist;
    }
    /// <summary>
    /// Check if there is an existing Student's Facebook account
    /// </summary>
    /// <param name="googleId"></param>
    /// <returns>Return true if Student's Facebook account exists</returns>
    public bool CheckFacebookExist(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckFacebookExist(googleId);
        return exist;
    }

    /// <summary>
    /// Insert Student record who logged in through Google
    /// </summary>
    /// <param name="studentName"></param>
    /// <param name="charId"></param>
    /// <param name="studentEmail"></param>
    /// <param name="studentUsername"></param>
    /// <param name="studentPassword"></param>
    /// <returns>Return int result 1 if insertion query has executed successfully</returns>
    public int InsertGoogleStudent(string studentName, string studentEmail, string googleId)
    {
        StudentDao studentDao = new StudentDao();
        int result = studentDao.InsertGoogleStudent(studentName, studentEmail, googleId);
        return result;
    }
    /// <summary>
    /// Insert Student record who logged in through Facebook
    /// </summary>
    /// <param name="studentName"></param>
    /// <param name="studentEmail"></param>
    /// <param name="fbId"></param>
    /// <returns>Return int result 1 if insertion query has executed successfully</returns>
    public int InsertFacebookStudent(string studentName, string studentEmail, string fbId)
    {
        StudentDao studentDao = new StudentDao();
        int result = studentDao.InsertFacebookStudent(studentName, studentEmail, fbId);
        return result;
    }
    /// <summary>
    /// Get existing Student's Google account
    /// </summary>
    /// <param name="googleId"></param>
    /// <returns>Return Student Object</returns>
    public int GetGoogleStudentId(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        return studentDao.GetGoogleStudent(googleId).StudentId;
    }
    /// <summary>
    /// Get existing Student's Facebook account
    /// </summary>
    /// <param name="fbId"></param>
    /// <returns>Retrun Student Object</returns>
    public int GetFacebookStudentId(string fbId)
    {
        StudentDao studentDao = new StudentDao();
        return studentDao.GetFacebookStudent(fbId).StudentId;
    }
    /// <summary>
    /// Updates Student's Character
    /// </summary>
    /// <param name="charId"></param>
    /// <param name="studentId"></param>
    /// <returns>Return 1 if update query has executed successfully</returns>
    public int UpdateStudentCharacter(int charId, int studentId){
        StudentDao studentDao = new StudentDao();
        return studentDao.UpdateStudentCharacter(charId, studentId);
    }
    /// <summary>
    /// Get Character that belongs to selected Student
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return Student object containing Character object</returns>
    public Student GetStudentCharacter(int studentId)
    {
        StudentDao studentDao = new StudentDao();
        return studentDao.GetStudentCharacter(studentId);
    }


}
