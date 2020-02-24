using System;
using System.Collections.Generic;

public class CustomLevelBL 
{
	public CustomLevel GetCustomLevel(int customLevelId)
	{
		CustomLevelDao customLevelDao = new CustomLevelDao();
		return customLevelDao.GetCustomLevel(customLevelId);
	}
}
