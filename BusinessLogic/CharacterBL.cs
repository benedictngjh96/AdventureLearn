using System;

public class CharacterBL
{
    public Character GetCharacter(int studentId)
    {
        CharacterDao characterDao = new CharacterDao();
        return characterDao.GetCharacter(studentId);
    }
}
