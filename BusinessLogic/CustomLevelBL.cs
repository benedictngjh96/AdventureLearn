using System;
using System.Collections.Generic;

public class CustomLevelBL 
{
	/// <summary>
	/// Return CustomLevel object according to customLevelId
	/// </summary>
	/// <param name="customLevelId"></param>
	/// <returns></returns>
	public CustomLevel GetCustomLevel(int customLevelId)
	{
		CustomLevelDao customLevelDao = new CustomLevelDao();
		return customLevelDao.GetCustomLevel(customLevelId);
	}
}
