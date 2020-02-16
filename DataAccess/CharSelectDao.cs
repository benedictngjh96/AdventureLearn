using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Godot;
using Dapper;
using System.Linq;

public class CharSelectDao : Node
{
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
