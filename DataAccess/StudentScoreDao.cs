using Godot;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class StudentScoreDao : Node
{
	public Student GetStudentScores(int sectionId, int studentId)
	{
		string query = String.Format("SELECT s.StudentId, ss.WorldId , ss.SectionId , ss.LevelId , ss.LevelScore FROM Student s INNER JOIN StudentScore  ss ON s.StudentId  = ss.StudentId WHERE ss.SectionId = {0} AND s.StudentId = {1}", sectionId, studentId);
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			var lookup = new Dictionary<int, Student>();
			conn.Query<Student, StudentScore, Student>(query, (s, ss) =>
			{
				Student student;
				if (!lookup.TryGetValue(s.StudentId, out student))
					lookup.Add(s.StudentId, student = s);
				if (student.StudentScore == null)
					student.StudentScore = new List<StudentScore>();
				student.StudentScore.Add(ss); /* Add locations to course */
				return student;
			}, splitOn: "StudentId, WorldId, SectionId, LevelId").AsQueryable();
			Student stud = new Student();
			if (lookup.Count() == 0)
				return null;

			stud = lookup.ElementAt(0).Value;
			return stud;
		}
	}
	public List<Student> GetAllStudentScores(int sectionId)
	{
		string query = String.Format("SELECT s.StudentId, ss.WorldId , ss.SectionId , ss.LevelId , ss.LevelScore FROM Student s INNER JOIN StudentScore  ss ON s.StudentId  = ss.StudentId WHERE ss.SectionId = {0}", sectionId);
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			var lookup = new Dictionary<int, Student>();
			conn.Query<Student, StudentScore, Student>(query, (s, ss) =>
			{
				Student student;
				if (!lookup.TryGetValue(s.StudentId, out student))
					lookup.Add(s.StudentId, student = s);
				if (student.StudentScore == null)
					student.StudentScore = new List<StudentScore>();
				student.StudentScore.Add(ss); /* Add locations to course */
				return student;
			}, splitOn: "StudentId").AsQueryable();
			List<Student> studentList = new List<Student>();
			studentList.AddRange(lookup.Values);

			return studentList;
		}
	}
}
