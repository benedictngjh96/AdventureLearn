using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class CharacterDao 
{
	/// <summary>
	/// Return Character object according to studentId
	/// </summary>
	/// <param name="studentId"></param>
	/// <returns></returns>
	public Character GetCharacter(string studentId)
	{
		BaseDao<Character> baseDao = new BaseDao<Character>();
		string query = String.Format("SELECT c.CharId, c.CharName, c.CharSkill FROM Student s "
			+ " INNER JOIN Characters c ON s.CharId = c.CharId WHERE s.StudentID = {0}", studentId);
		Character character = baseDao.RetrieveQuery(query);
		return character;
	}
	/// <summary>
	/// Return list of Character
	/// </summary>
	/// <returns></returns>
	public List<Character> GetAllCharacters()
	{
		List<Character> characterList;

		string query = "SELECT * FROM Characters";
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			characterList = conn.Query<Character>(query).ToList();
		}
		return characterList;
	}
}
