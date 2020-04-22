using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for Character
/// </summary>
public class CharacterBL
{
    /// <summary>
    /// Get Student's Character
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return Character object</returns>
    public Character GetCharacter(int studentId)
    {
        CharacterDao characterDao = new CharacterDao();
        return characterDao.GetCharacter(studentId);
    }
    /// <summary>
    /// Get all of the Characters
    /// </summary>
    /// <returns>Return list of Character object</returns>
    public List<Character> GetAllCharacters()
    {
        CharacterDao characterDao = new CharacterDao();
        return characterDao.GetAllCharacters();
    }

    /// <summary>
    /// Get all of the Monsters
    /// </summary>
    /// <returns>Return list of Monster object</returns>
    public List<Monster> GetAllMonsters()
    {
        CharacterDao characterDao = new CharacterDao();
        return characterDao.GetAllMonsters();
    }
}
