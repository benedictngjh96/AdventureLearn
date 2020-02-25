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
}
