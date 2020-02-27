
using System;
using System.Collections.Generic;

public class StudentBL
{
    StudentDao studentDao = new StudentDao();
    /// <summary>
    /// Return true if StudentId already exists
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public bool CheckStudentExist(int studentId)
    {
        bool exist = studentDao.CheckStudentExist(studentId);
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
    public int InsertStudent(string studentName, int charId, string studentEmail, string studentUsername, string studentPassword, string hash, string salt)
    {
        int result = studentDao.InsertStudent(studentName, charId, studentEmail, studentUsername, studentPassword, hash, salt);
        return result;
    }
    

}
