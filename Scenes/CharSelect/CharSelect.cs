using Godot;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Net;

public class CharSelect : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var csb = new MySqlConnectionStringBuilder
		{
			Server = "35.198.238.34",
			UserID = "root",
			Password = "MpiPkr9y04xmg11h",
			Database = "test",
			SslMode = MySqlSslMode.None,
		};
		try{
			using (var connection = new MySqlConnection(csb.ConnectionString))
			{
				connection.Open();
				GD.Print("con");
				
				MySqlCommand cmd = new MySqlCommand("SELECT UserName FROM User WHERE UserId =4",connection);
				using(MySqlDataReader reader = cmd.ExecuteReader()){
					if(reader.Read()){
						//GD.Print(String.Format("{0}",reader["UserName"]));
						var lbl = (Label)GetNode("Label");
						//lbl.Text = String.Format("{0}",reader["UserName"]);
					}
				}
			}
			
		}
		catch(Exception ex){
			GD.Print(ex.Message);
		}

	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
