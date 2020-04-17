using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
public class StudentDao
{
    /// <summary>
    /// Return int result 1 if InsertStudent has executed successfully
    /// </summary>
    /// <param name="studentName"></param>
    /// <param name="charId"></param>
    /// <param name="studentEmail"></param>
    /// <param name="studentUsername"></param>
    /// <param name="studentPassword"></param>
    /// <returns></returns>
    public int InsertGoogleStudent(string studentName, string studentEmail, string googleId)
    {
        BaseDao<Student> baseDao = new BaseDao<Student>();
        string query = "INSERT INTO Student (StudentName, StudentEmail, GoogleAccountId) " +
            "VALUES (@StudentName, @StudentEmail, @GoogleAccountId)";
        int result = baseDao.ExecuteQuery(query, new { StudentName = studentName, StudentEmail = studentEmail, GoogleAccountId = googleId });
        return result;

    }
    public int InsertFacebookStudent(string studentName, string studentEmail, string fbId)
    {
        BaseDao<Student> baseDao = new BaseDao<Student>();
        string query = "INSERT INTO Student (StudentName, StudentEmail, FacebookAccountId) " +
            "VALUES (@StudentName, @StudentEmail, @FacebookAccountId)";
        int result = baseDao.ExecuteQuery(query, new { StudentName = studentName, StudentEmail = studentEmail, FacebookAccountId = fbId });
        return result;

    }
    public Student GetLastStudent()
    {
        BaseDao<Student> baseDao = new BaseDao<Student>();
        Student student = baseDao.RetrieveQuery("SELECT * FROM Student ORDER BY StudentId DESC LIMIT 1");
        return student;
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
    /// Return true if student exist
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public bool CheckStudentExist(int studentId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE StudentId = @StudentId";
        BaseDao<bool> baseDao = new BaseDao<bool>();
        var studentObj = new { StudentId = studentId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    public bool CheckGoogleCharExist(string googleId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE GoogleAccountId = @GoogleAccountId AND CharId != ''";
        BaseDao<bool> baseDao = new BaseDao<bool>();
        var studentObj = new { GoogleAccountId = googleId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    public bool CheckFacebookCharExist(string fbId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE FacebookAccountId = @FacebookAccountId AND CharId != ''";
        BaseDao<bool> baseDao = new BaseDao<bool>();
        var studentObj = new { FacebookAccountId = fbId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    public bool CheckGoogleExist(string googleId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE GoogleAccountId = @GoogleAccountId";
        BaseDao<bool> baseDao = new BaseDao<bool>();
        var studentObj = new { GoogleAccountId = googleId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    public bool CheckFacebookExist(string fbId)
    {
        string query = "SELECT COUNT(1) FROM Student WHERE FacebookAccountId = @FacebookAccountId";
        BaseDao<bool> baseDao = new BaseDao<bool>();
        var studentObj = new { FacebookAccountId = fbId };
        bool exist = baseDao.ExecuteScalar(query, studentObj);
        return exist;
    }
    public Student GetStudent()
    {
        BaseDao<Student> baseDao = new BaseDao<Student>();
        Student student = baseDao.RetrieveQuery("SELECT * FROM Student WHERE StudentId = 4");
        return student;
    }
    public List<Student> GetStudents()
    {
        List<Student> studentList = new List<Student>();
        string query = "SELECT * FROM Student";
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            studentList = conn.Query<Student>(query).ToList();
        }
        return studentList;
    }
    public Student GetFacebookStudent(string fbId)
    {
        BaseDao<Student> baseDao = new BaseDao<Student>();
        string query = String.Format("SELECT * FROM Student WHERE FacebookAccountId = {0}", fbId);
        Student student = baseDao.RetrieveQuery(query);
        return student;
    }
    public Student GetGoogleStudent(string googleId)
    {
        BaseDao<Student> baseDao = new BaseDao<Student>();
        string query = String.Format("SELECT * FROM Student WHERE GoogleAccountId = {0}", googleId);
        Student student = baseDao.RetrieveQuery(query);
        return student;
    }
    public int UpdateStudentCharacter(int charId, int studentId)
    {
        BaseDao<Object> baseDao = new BaseDao<Object>();
        string query = "UPDATE Student SET CharId = @CharId WHERE StudentId = @StudentId";
        int result = baseDao.ExecuteQuery(query, new { CharId = charId, StudentId = studentId });
        return result;
    }
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
