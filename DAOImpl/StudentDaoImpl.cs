using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;

/// <summary>
/// Class to handle DAO operations for Student
/// </summary>
public class StudentDaoImpl
{
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
        BaseDaoImpl<Student> baseDao = new BaseDaoImpl<Student>();
        string query = "INSERT INTO Student (StudentName, StudentEmail, GoogleAccountId) " +
            "VALUES (@StudentName, @StudentEmail, @GoogleAccountId)";
        int result = baseDao.ExecuteQuery(query, new { StudentName = studentName, StudentEmail = studentEmail, GoogleAccountId = googleId });
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
        BaseDaoImpl<Student> baseDao = new BaseDaoImpl<Student>();
        string query = "INSERT INTO Student (StudentName, StudentEmail, FacebookAccountId) " +
            "VALUES (@StudentName, @StudentEmail, @FacebookAccountId)";
        int result = baseDao.ExecuteQuery(query, new { StudentName = studentName, StudentEmail = studentEmail, FacebookAccountId = fbId });
        return result;

    }

    /// <summary>
    /// Check if Student has an existing account
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return true if Student's record exist</returns>
    public bool CheckStudentExist(int studentId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE StudentId = @StudentId";
        BaseDaoImpl<bool> baseDao = new BaseDaoImpl<bool>();
        var studentObj = new { StudentId = studentId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    /// <summary>
    /// Check if Student who has logged in with Google account has an existing Character
    /// </summary>
    /// <param name="googleId"></param>
    /// <returns>Return true if Student has existing Character</returns>
    public bool CheckGoogleCharExist(string googleId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE GoogleAccountId = @GoogleAccountId AND CharId != ''";
        BaseDaoImpl<bool> baseDao = new BaseDaoImpl<bool>();
        var studentObj = new { GoogleAccountId = googleId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    /// <summary>
    /// Check if Student who has logged in with Facebook account has an existing Chrracter
    /// </summary>
    /// <param name="fbId"></param>
    /// <returns>Return true if Student has existing Character</returns>
    public bool CheckFacebookCharExist(string fbId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE FacebookAccountId = @FacebookAccountId AND CharId != ''";
        BaseDaoImpl<bool> baseDao = new BaseDaoImpl<bool>();
        var studentObj = new { FacebookAccountId = fbId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    /// <summary>
    /// Check if there is an existing Student's Google account
    /// </summary>
    /// <param name="googleId"></param>
    /// <returns>Return true if Student's Google account exists</returns>
    public bool CheckGoogleExist(string googleId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE GoogleAccountId = @GoogleAccountId";
        BaseDaoImpl<bool> baseDao = new BaseDaoImpl<bool>();
        var studentObj = new { GoogleAccountId = googleId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    /// <summary>
    /// Check if there is an existing Student's Facebook account
    /// </summary>
    /// <param name="fbId"></param>
    /// <returns>Return true if Student's Facebook account exists</returns>
    public bool CheckFacebookExist(string fbId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE FacebookAccountId = @FacebookAccountId";
        BaseDaoImpl<bool> baseDao = new BaseDaoImpl<bool>();
        var studentObj = new { FacebookAccountId = fbId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    /// <summary>
    /// Get existing Student's Facebook account
    /// </summary>
    /// <param name="fbId"></param>
    /// <returns>Retrun Student Object</returns>
    public Student GetFacebookStudent(string fbId)
    {
        BaseDaoImpl<Student> baseDao = new BaseDaoImpl<Student>();
        string query = String.Format("SELECT * FROM Student WHERE FacebookAccountId = {0}", fbId);
        Student student = baseDao.RetrieveQuery(query);
        return student;
    }
    /// <summary>
    /// Get existing Student's Google account
    /// </summary>
    /// <param name="googleId"></param>
    /// <returns>Return Student Object</returns>
    public Student GetGoogleStudent(string googleId)
    {
        BaseDaoImpl<Student> baseDao = new BaseDaoImpl<Student>();
        string query = String.Format("SELECT * FROM Student WHERE GoogleAccountId = {0}", googleId);
        Student student = baseDao.RetrieveQuery(query);
        return student;
    }
    /// <summary>
    /// Updates Student's Character
    /// </summary>
    /// <param name="charId"></param>
    /// <param name="studentId"></param>
    /// <returns>Return 1 if update query has executed successfully</returns>
    public int UpdateStudentCharacter(int charId, int studentId)
    {
        BaseDaoImpl<Object> baseDao = new BaseDaoImpl<Object>();
        string query = "UPDATE Student SET CharId = @CharId WHERE StudentId = @StudentId";
        int result = baseDao.ExecuteQuery(query, new { CharId = charId, StudentId = studentId });
        return result;
    }
    /// <summary>
    /// Get Character that belongs to selected Student
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return Student object containing Character object</returns>
    public Student GetStudentCharacter(int studentId)
    {
        Student student = new Student();
        List<Student> studentList = new List<Student>();
        string query = String.Format("SELECT s.StudentId , s.StudentName , s.StudentEmail , c.CharId , c.CharName , c.CharSkill, c.SkillDescription FROM Student s " +
        "INNER JOIN `Characters` c ON s.CharId  = c.CharId WHERE s.StudentId  = {0}", studentId);
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            studentList = conn.Query<Student, Character, Student>(query, (s, c) =>
            {
                s.Character = c;
                return s;
            }, splitOn: "StudentId , CharId").Distinct().ToList();
        }
        return studentList[0];
    }
}
