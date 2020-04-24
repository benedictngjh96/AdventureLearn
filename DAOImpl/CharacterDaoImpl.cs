using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

/// <summary>
/// Class to handle DAO operations for Character
/// </summary>
public class CharacterDaoImpl 
{
	/// <summary>
	/// Get Student's character
	/// </summary>
	/// <param name="studentId"></param>
	/// <returns>Return Character object</returns>
	public Character GetCharacter(int studentId)
	{
		BaseDaoImpl<Character> baseDao = new BaseDaoImpl<Character>();
		string query = String.Format("SELECT c.CharId, c.CharName, c.CharSkill FROM Student s "
			+ " INNER JOIN Characters c ON s.CharId = c.CharId WHERE s.StudentID = {0}", studentId);
		Character character = baseDao.RetrieveQuery(query);
		return character;
	}
	/// <summary>
	/// Get all Characters
	/// </summary>
	/// <returns>Return list of Character object</returns>
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

	/// <summary>
	/// Get all Monsters
	/// </summary>
	/// <returns>Return list of Monster object</returns>
	public List<Monster> GetAllMonsters()
	{
		List<Monster> monsterList;

		string query = "SELECT * FROM Monster";
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			monsterList = conn.Query<Monster>(query).ToList();
		}
		return monsterList;
	}
}
