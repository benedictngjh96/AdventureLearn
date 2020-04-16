using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class AssignmentDao
{
    /// <summary>
    /// Query to return Assignment object containing Monster object and Question object
    /// </summary>
    /// <param name="assignmentId"></param>
    /// <returns></returns>
    public Assignment GetAssignment(int assignmentId)
    {
        string query = String.Format("SELECT a.AssignmentId ,a.AssignmentName, a.TimeLimit, m.MonsterId ,m.MonsterName , q.QuestionId ,q.QuestionTitle " +
            ",q.Option1 ,q.Option2 ,q.Option3, q.CorrectOption  " +
            "FROM Assignment a INNER JOIN TeacherCustomQuestion tcq ON a.AssignmentId  = tcq.AssignmentId " +
            "INNER JOIN Monster m ON m.MonsterId  = a.MonsterId INNER JOIN Question q ON tcq.QuestionId  = q.QuestionId  " +
            "WHERE a.AssignmentId  = {0}", assignmentId);
        Dictionary<int, Assignment> assignmentDict;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            assignmentDict = new Dictionary<int, Assignment>();
            var list = conn.Query<Assignment, Monster, Question, Assignment>(
                query,
                (a, m, q) =>
                {
                    Assignment assignment;
                    if (!assignmentDict.TryGetValue(a.AssignmentId, out assignment))
                    {
                        assignment = a;
                        assignment.Question = new List<Question>();
                        assignmentDict.Add(assignment.AssignmentId, assignment);
                    }
                    assignment.Question.Add(q);
                    assignment.Monster = m;
                    return assignment;
                }, splitOn: "AssignmentId, MonsterId, QuestionId").Distinct().ToList();
        }
        return assignmentDict[assignmentId];
    }
    public List<Assignment> GetAssignments()
    {
        string query = "SELECT * FROM Assignment";
        List<Assignment> assignments;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            assignments = conn.Query<Assignment>(query).ToList();
        }
        return assignments;
    }
    public List<PublishedAssignment> GetStudentAssignment(int studentId)
    {
        string query = String.Format("SELECT DueDate, AssignmentId , AssignmentName , ClassId, TeacherId ,TeacherName " +
        "FROM Assignment NATURAL JOIN Teacher NATURAL JOIN PublishedAssignment NATURAL JOIN BelongClass " +
        "WHERE StudentId  = {0} ORDER BY DueDate DESC", studentId);
        int count = 0;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            var lookup = new Dictionary<int, PublishedAssignment>();
            conn.Query<PublishedAssignment, Assignment, Teacher, PublishedAssignment>(query, (pa, a, t) =>
            {
                PublishedAssignment assignment;
                if (!lookup.TryGetValue(count, out assignment))
                {
                    lookup.Add(count, assignment = pa);
                }
                assignment.Assignment = a;
                assignment.Assignment.Teacher = t;
                count++;
                return assignment;
            }, splitOn: "AssignmentId, TeacherId").AsQueryable();
            List<PublishedAssignment> assignmentList = new List<PublishedAssignment>();
            assignmentList.AddRange(lookup.Values);
            return assignmentList;
        }
    }
    /// <summary>
    /// Return int value 1 if InsertAssignment has executed successfully
    /// </summary>
    /// <param name="teacherId"></param>
    /// <param name="assignmentName"></param>
    /// <param name="dueDate"></param>
    /// <param name="monsterId"></param>
    /// <param name="timeLimit"></param>
    /// <param name="question"></param>
    /// <returns></returns>
    public int InsertAssignment(int teacherId, string assignmentName, DateTime dueDate, int monsterId, int timeLimit, List<Question> question)
    {
        return 1;
    }
    public Monster GetAssignmentMonster(int assignmentId)
    {
        BaseDao<Monster> baseDao = new BaseDao<Monster>();
        string query = String.Format("SELECT m.MonsterId, m.MonsterName FROM Assignment a NATURAL JOIN Monster m " +
        "WHERE a.AssignmentId = {0}", assignmentId);
        Monster monster = baseDao.RetrieveQuery(query);
        return monster;
    }
    
}
