using System;
using System.Collections.Generic;

public class CharacterBL
{
    /// <summary>
    /// Return Character object according to studentId
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public Character GetCharacter(int studentId)
    {
        CharacterDao characterDao = new CharacterDao();
        return characterDao.GetCharacter(studentId);
    }
    /// <summary>
    /// Return list of Character object
    /// </summary>
    /// <returns></returns>
    public List<Character> GetAllCharacters()
    {
        CharacterDao characterDao = new CharacterDao();
        return characterDao.GetAllCharacters();
    }
}
