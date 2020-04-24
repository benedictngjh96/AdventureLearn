using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Presentation for GamePlay
/// </summary>
public class GamePlay : Node2D
{
    List<string> animationList;
    List<Question> questionList;
    AnimatedSprite charSprite;
    AnimatedSprite monsterSprite;
    AnimatedSprite charSkillSprite;
    AnimatedSprite countDown;
    AnimatedSprite monsterSkillSprite;
    AnimatedSprite win;
    AnimatedSprite lose;
    AnimatedSprite activeSkill;

    Button op1;
    Button op2;
    Button op3;
    Button op4;
    Character character;
    Question question;
    Label timerLabel;
    Label questionLabel;
    Label levelTitle;

    Label timerLbl2;
    Sprite tick;
    Sprite tick2;
    Sprite tick3;
    Sprite tick4;
    Sprite cross;
    Sprite cross2;
    Sprite cross3;
    Sprite cross4;

    Theme theme1;
    Theme theme2;
    Theme theme3;

    Timer timer;
    Timer timer2;

    PopupMenu popupMenu;
    PopupMenu losePopup;

    int questionIndex = 0;
    int correctAnsIndex = 0;
    string spritePath = "res://CharSprites/";
    int timeLimit = 0;
    int s = 0;
    int ms = 0;
    string gameType = "";
    bool shieldOn = false;
    AudioStreamPlayer audioStreamPlayer;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
        audioStreamPlayer.VolumeDb = Global.SfxVol;
        AudioStream sfx = ResourceLoader.Load("res://Assets/SoundEffects/battleThemeA.ogg") as AudioStream;
        DefaultSound.playSound(sfx);


        AddUserSignal("NoMoreQuestions");
        charSprite = GetNode<AnimatedSprite>("CharSprite");
        monsterSprite = GetNode<AnimatedSprite>("MonsterSprite");
        charSkillSprite = GetNode<AnimatedSprite>("CharSprite/CharSkillSprite");
        monsterSkillSprite = GetNode<AnimatedSprite>("MonsterSprite/MonsterSkillSprite");
        activeSkill = GetNode<AnimatedSprite>("CharSprite/ActiveSkill");
        countDown = GetNode<AnimatedSprite>("Countdown");
        timerLabel = GetNode<Label>("TimerLabel");
        questionLabel = GetNode<Label>("QuestionLabel");
        levelTitle = GetNode<Label>("LevelTitle");
        timerLbl2 = GetNode<Label>("TimerLabel2");
        popupMenu = GetNode<PopupMenu>("PopupMenu");
        losePopup = GetNode<PopupMenu>("LosePopup");
        win = GetNode<AnimatedSprite>("Win");
        lose = GetNode<AnimatedSprite>("Lose");
        tick = GetNode<Sprite>("Ans/Tick");
        cross = GetNode<Sprite>("Ans/Cross");
        tick2 = GetNode<Sprite>("Ans/Tick2");
        cross2 = GetNode<Sprite>("Ans/Cross2");
        tick3 = GetNode<Sprite>("Ans/Tick3");
        cross3 = GetNode<Sprite>("Ans/Cross3");
        tick4 = GetNode<Sprite>("Ans/Tick4");
        cross4 = GetNode<Sprite>("Ans/Cross4");
        timer2 = GetNode<Timer>("Timer2");
        animationList = new List<string>();
        animationList.Add("Idle");
        animationList.Add("Hurt");
        animationList.Add("Die");
        animationList.Add("Attack");

        op1 = GetNode<Button>("Buttons/Option1");
        op2 = GetNode<Button>("Buttons/Option2");
        op3 = GetNode<Button>("Buttons/Option3");
        op4 = GetNode<Button>("Buttons/Option4");

        theme1 = ResourceLoader.Load("res://Assets/GUI/BtnUI.tres") as Theme;
        theme2 = ResourceLoader.Load("res://Assets/GUI/BtnUI2.tres") as Theme;
        theme3 = ResourceLoader.Load("res://Assets/GUI/BtnUI3.tres") as Theme;
        DisplayBtnDesign();
        countDown.Play("Countdown");
        timer = GetNode<Timer>("TimeLimit");
    }

    /// <summary>
    /// Handles the countdown logic
    /// </summary>
    /// <param name="delta"></param>
    public override void _Process(float delta)
    {
        if (s <= 0)
        {
            //GetTree().ChangeScene("res://Presentation/World/World.tscn");
            charSprite.Play("Die");
            HideBtns();
            lose.Play("Lose");
            timerLabel.Visible = false;

        }
        if (ms <= 0)
        {
            s -= 1;
            ms = 9;
        }
        timerLabel.Text = String.Format("{0}:{1}", s, ms);

        if (s <= 10)
        {
            //RED
            float r = 0.73f;
            float g = 0.03f;
            float b = 0.11f;
            timerLabel.AddColorOverride("font_color", new Color(r, g, b));
        }


    }

    /// <summary>
    /// Set Level Title
    /// </summary>
    /// <param name="title"></param>
    public void SetLevelTitle(string title)
    {
        levelTitle.Text = title;
    }

    /// <summary>
    /// Set Question Number
    /// </summary>
    public void SetQuestionNum()
    {
        questionLabel.Text = String.Format("{0}/{1}", questionIndex + 1, questionList.Count);
    }

    /// <summary>
    /// Set time limit
    /// </summary>
    /// <param name="timeLimit"></param>
    public void SetTimeLimit(int timeLimit)
    {

        s = timeLimit;
        this.timeLimit = timeLimit;
    }

    /// <summary>
    /// Set Question list
    /// </summary>
    /// <param name="questionList"></param>
    public void SetQuestionList(List<Question> questionList)
    {
        this.questionList = questionList;
    }

    /// <summary>
    /// Display the Question
    /// </summary>
    public void DisplayQuestion()
    {
        Control controlParentNode = GetNode<Control>("Buttons");
        List<string> optionList = new List<string>();
        Label questionTitle = GetNode<Label>("QuestionTitle");
        question = questionList[questionIndex];
        optionList.Add(question.Option1);
        optionList.Add(question.Option2);
        optionList.Add(question.Option3);
        optionList.Add(question.CorrectOption);
        questionTitle.Text = questionList[questionIndex].QuestionTitle;
        Random r = new Random();

        //Shuffle options
        foreach (Node n in controlParentNode.GetChildren())
        {
            if (n is Button)
            {
                Button btn = n as Button;
                int rng = r.Next(optionList.Count);
                btn.Text = optionList[rng];
                //optionButton.Text = optionList[rng];
                if (optionList[rng] == question.CorrectOption)
                    correctAnsIndex = rng + 1;

                optionList.RemoveAt(rng);
            }
        }
    }

    /// <summary>
    /// Display the Student's character sprite
    /// </summary>
    /// <param name="character"></param>
    public void DisplayCharSprite(Character character)
    {
        string charPath = String.Format(spritePath + "{0}/", character.CharName);
        Global.LoadSprite(charPath, charSprite, animationList);
        if (character.CharName == "Mjolnir")
            charSprite.Position = new Vector2(351, 289);
        else if (character.CharName == "Zeus")
            charSprite.Position = new Vector2(355, 289);
        else if (character.CharName == "Escanor")
            charSprite.Position = new Vector2(370, 280);
    }

    /// <summary>
    /// Display the Monster sprite
    /// </summary>
    /// <param name="monster"></param>
    public void DisplayMonsterSprite(Monster monster)
    {
        string monsterPath = String.Format(spritePath + "{0}/", monster.MonsterName);
        Global.LoadSprite(monsterPath, monsterSprite, animationList);
    }

    /// <summary>
    /// Display next Question
    /// </summary>
    public void DisplayNextQuestion()
    {
        Control ctr = GetNode<Control>("Buttons");

        if (questionIndex < questionList.Count - 1)
        {
            questionIndex++;
            DisplayQuestion();
            SetQuestionNum();
        }
        else
        {
            //EmitSignal("NoMoreQuestions")
            timerLabel.Visible = false;
            HideBtns();
            monsterSprite.Play("Die");
            win.Play("Win");
            DefaultSound.disableSound();
            AudioStream sfx = ResourceLoader.Load("res://Assets/SoundEffects/Jingle_Win_00.wav") as AudioStream;
            audioStreamPlayer.Stream = sfx;
            audioStreamPlayer.Playing = true;
        }
    }

    /// <summary>
    /// Hide the buttons
    /// </summary>
    private void HideBtns()
    {
        op1.Visible = false;
        op2.Visible = false;
        op3.Visible = false;
        op4.Visible = false;
    }

    /// <summary>
    /// Display the themed buttons
    /// </summary>
    private void DisplayBtnDesign()
    {
        if (Global.SectionId == 1)
        {
            op1.Theme = theme1;
            op2.Theme = theme1;
            op3.Theme = theme1;
            op4.Theme = theme1;
        }
        else if (Global.SectionId == 2)
        {
            op1.Theme = theme2;
            op2.Theme = theme2;
            op3.Theme = theme2;
            op4.Theme = theme2;
        }
        else
        {
            op1.Theme = theme3;
            op2.Theme = theme3;
            op3.Theme = theme3;
            op4.Theme = theme3;
        }
    }

    /// <summary>
    /// Handles the logic when the win animation finishes
    /// </summary>
    private void _on_Win_animation_finished()
    {
        win.Stop();
        EmitSignal("NoMoreQuestions");
        s = 1000;
        popupMenu.Show();
        HideNextButton();
    }

    /// <summary>
    /// Handles the logic when the lose animation finishes
    /// </summary>
    private void _on_Lose_animation_finished()
    {
        s = 1000;
        lose.Stop();
        losePopup.Show();
        HideNextButton();
        DefaultSound.disableSound();
        AudioStream sfx = ResourceLoader.Load("res://Assets/SoundEffects/Jingle_Lose_00.wav") as AudioStream;
        audioStreamPlayer.Stream = sfx;
        audioStreamPlayer.Playing = true;
    }

    /// <summary>
    /// Hide the Next button in popup menu
    /// </summary>
    private void HideNextButton()
    {
        TextureButton btn = GetNode<TextureButton>("PopupMenu/Next");
        switch (gameType)
        {
            case "Assignment":
                btn.Visible = false;
                break;
            case "CustomLevel":
                btn.Visible = false;
                break;
        }
    }

    /// <summary>
    /// Check if the answer is correct and play attack animations for Character/Monster and sound effects
    /// </summary>
    /// <param name="option"></param>
    /// <returns>Return true if the answer, else return false if the answer is wrong</returns>
    public bool CheckCorrectAnswer(string option)
    {
        Random r = new Random();
        int random = r.Next(1, 4);
        bool result = false;

        if (option == question.CorrectOption)
        {
            //UNDISABLE
            op1.Disabled = false;
            op2.Disabled = false;
            op3.Disabled = false;
            op4.Disabled = false;

            charSprite.Play("Attack");
            monsterSprite.Play("Hurt");
            monsterSkillSprite.Play(String.Format("Explosion{0}", random));
            DisplayNextQuestion();
            AudioStream sfx = ResourceLoader.Load("res://Assets/SoundEffects/Spell_02.wav") as AudioStream;
            audioStreamPlayer.Stream = sfx;
            result = true;
        }
        else
        {
            monsterSprite.Play("Attack");
            result = false;
            charSkillSprite.Play(String.Format("Explosion{0}", random));
            if (!shieldOn)
            {
                charSprite.Play("Hurt");
                s -= 10;
            }
            else
            {
                activeSkill.Play("default");
            }
            AudioStream sfx = ResourceLoader.Load("res://Assets/SoundEffects/Spell_04.wav") as AudioStream;
            audioStreamPlayer.Stream = sfx;
        }
        audioStreamPlayer.Playing = true;
        return result;
    }

    /// <summary>
    /// Set the game type
    /// </summary>
    /// <param name="gameType"></param>
    public void SetGameType(string gameType)
    {
        this.gameType = gameType;
    }

    /// <summary>
    /// Get remaining time left
    /// </summary>
    /// <returns></returns>
    public int GetTimeLeft()
    {
        return s;
    }

    /// <summary>
    /// Set Character
    /// </summary>
    /// <param name="character"></param>
    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    /// <summary>
    /// Handles the logic when option1 is pressed
    /// </summary>
    private void _on_Option1_pressed()
    {
        if (CheckCorrectAnswer(op1.Text))
        {
            tick.Visible = true;
            timer2.Start();
        }
        else
        {
            cross.Visible = true;
            if (shieldOn != true)
                timerLbl2.Visible = true;
            else
                shieldOn = false;

            timer2.Start();
        }
    }

    /// <summary>
    /// Handles the logic when option2 is pressed
    /// </summary>
    private void _on_Option2_pressed()
    {
        if (CheckCorrectAnswer(op2.Text))
        {
            tick2.Visible = true;
            timer2.Start();
        }
        else
        {
            cross2.Visible = true;
            if (shieldOn != true)
                timerLbl2.Visible = true;
            else
                shieldOn = false;
            timer2.Start();
        }
    }

    /// <summary>
    /// Display the stars
    /// </summary>
    /// <param name="star"></param>
    public void DisplayStars(int star)
    {
        Sprite starSprite = GetNode<Sprite>("PopupMenu/Star");
        switch (star)
        {
            case 1:
                Texture texture2 = ResourceLoader.Load("res://Assets/GameplayUI/Popup/star_3.png") as Texture;
                starSprite.Texture = texture2;
                break;
            case 2:
                Texture texture = ResourceLoader.Load("res://Assets/GameplayUI/Popup/star_2.png") as Texture;
                starSprite.Texture = texture;
                break;
            case 3:
                Texture texture3 = ResourceLoader.Load("res://Assets/GameplayUI/Popup/star_1.png") as Texture;
                starSprite.Texture = texture3;
                break;

        }
    }

    /// <summary>
    /// Handles the logic when option3 is pressed
    /// </summary>
    private void _on_Option3_pressed()
    {
        if (CheckCorrectAnswer(op3.Text))
        {
            tick3.Visible = true;
            timer2.Start();
        }
        else
        {
            cross3.Visible = true;
            if (shieldOn != true)
                timerLbl2.Visible = true;
            else
                shieldOn = false;
            timer2.Start();
        }
    }

    /// <summary>
    /// Handles the logic when option4 is pressed
    /// </summary>
    private void _on_Option4_pressed()
    {
        if (CheckCorrectAnswer(op4.Text))
        {
            tick4.Visible = true;
            timer2.Start();
        }
        else
        {
            cross4.Visible = true;
            if (shieldOn != true)
                timerLbl2.Visible = true;
            else
                shieldOn = false;
            timer2.Start();
        }
    }

    /// <summary>
    /// Reset the Student's Character Sprite to Idle animation when other animations has finish playing
    /// </summary>
    private void _on_CharSprite_animation_finished()
    {
        if (charSprite.IsPlaying() && charSprite.Animation != "Idle" && charSprite.Animation != "Die")
            charSprite.Play("Idle");
    }

    /// <summary>
    /// Reset the Monster Sprite to Idle animation when other animations has finish playing
    /// </summary>
    private void _on_MonsterSprite_animation_finished()
    {
        if (monsterSprite.IsPlaying() && monsterSprite.Animation != "Idle" && monsterSprite.Animation != "Die")
            monsterSprite.Play("Idle");

    }

    /// <summary>
    /// Decrement the time limit timer by 1ms
    /// </summary>
    private void _on_TimeLimit_timeout()
    {
        //s -= 1;
        ms -= 1;
    }

    /// <summary>
    /// Decrement the timer by 1ms
    /// </summary>
    private void _on_Timer_timeout()
    {
        //s -= 1;
        ms -= 1;
    }

    /// <summary>
    /// Hide the ticks upon timeout
    /// </summary>
    private void _on_Timer2_timeout()
    {
        tick.Visible = false;
        cross.Visible = false;
        tick2.Visible = false;
        cross2.Visible = false;
        tick3.Visible = false;
        cross3.Visible = false;
        tick4.Visible = false;
        cross4.Visible = false;
        timerLbl2.Visible = false;
        timer2.Stop();
    }

    /// <summary>
    /// Play default animation for Character when all the animations finish
    /// </summary>
    private void _on_CharSkillSprite_animation_finished()
    {
        if (!(charSkillSprite.Animation == "Shield"))
            charSkillSprite.Play("Default");
    }

    /// <summary>
    /// Play default animation for Monster when all the animations finish
    /// </summary>
    private void _on_MonsterSkillSprite_animation_finished()
    {
        monsterSkillSprite.Play("Default");
    }

    /// <summary>
    /// Load the skill icon
    /// </summary>
    private void loadSkillIcon()
    {
        TextureButton skillIcon = GetNode<TextureButton>("SkillBtn");
        string skillIconPath = "res://Skills/Icons/";
        Godot.GD.Print(skillIconPath + character.CharSkill + ".png");
        var texture = ResourceLoader.Load(skillIconPath + character.CharSkill + ".png") as Texture;
        var disabledTexture = ResourceLoader.Load(skillIconPath + character.CharSkill + "locked.png") as Texture;
        skillIcon.TextureNormal = texture;
        skillIcon.TexturePressed = texture;
        skillIcon.TextureHover = texture;
        skillIcon.TextureDisabled = disabledTexture;
    }

    /// <summary>
    /// Handles the logic when the skill button is pressed
    /// </summary>
    private void _on_SkillBtn_pressed()
    {
        switch (character.CharSkill)
        {
            case "Shield":
                activeSkill.Play("Shield");
                shieldOn = true;
                break;
            case "Zap":
                Random r = new Random();
                Button btn;
                int random;
                do
                {
                    random = r.Next(1, 4);
                    btn = GetNode<Button>("Buttons/Option" + random.ToString());
                }
                while (btn.Text == question.CorrectOption);

                AnimatedSprite effect = GetNode<AnimatedSprite>(String.Format("Buttons/Option{0}/Effect{0}", random));
                effect.Play("Explosion");
                btn.Disabled = true;
                break;
            case "Gamble":
                Random r2 = new Random();
                int rng2 = r2.Next(3);
                if (rng2 == 1)
                {
                    CheckCorrectAnswer(question.CorrectOption);
                }
                else
                {
                    CheckCorrectAnswer("WrongAnswer");
                }

                break;
            case "Heal":
                s += 10;
                break;
        }
        TextureButton skillBtn = GetNode<TextureButton>("SkillBtn");
        skillBtn.Disabled = true;
    }

    /// <summary>
    /// Play default animation when the Effect1 animation finishes
    /// </summary>
    private void _on_Effect1_animation_finished()
    {
        AnimatedSprite effect = GetNode<AnimatedSprite>("Buttons/Option1/Effect1");
        effect.Play("Default");
    }

    /// <summary>
    /// Play default animation when the Effect3 animation finishes
    /// </summary>
    private void _on_Effect3_animation_finished()
    {
        AnimatedSprite effect = GetNode<AnimatedSprite>("Buttons/Option3/Effect3");
        effect.Play("Default");
    }

    /// <summary>
    /// Play default animation when the Effect2 animation finishes
    /// </summary>
    private void _on_Effect2_animation_finished()
    {
        AnimatedSprite effect = GetNode<AnimatedSprite>("Buttons/Option2/Effect2");
        effect.Play("Default");
    }

    /// <summary>
    /// Play default animation when the Effect4 animation finishes
    /// </summary>
    private void _on_Effect4_animation_finished()
    {
        AnimatedSprite effect = GetNode<AnimatedSprite>("Buttons/Option4/Effect4");
        effect.Play("Default");
    }

    /// <summary>
    /// Set the background
    /// </summary>
    public void SetBg()
    {
        Node2D n = GetNode<Node2D>("Bg");
        Sprite s = GetNode<Sprite>("Bg/ParallaxBackground/ParallaxLayer/Sprite");

        if (Global.SectionId == 1)
        {
            var texture2 = ResourceLoader.Load("res://Assets/LevelUI/Bg/bg3.png") as Texture;
            s.Texture = texture2;
        }
        else if (Global.SectionId == 2)
        {
            var texture2 = ResourceLoader.Load("res://Assets/LevelUI/Bg/bg1.png") as Texture;
            s.Texture = texture2;
        }
        else
        {
            var texture2 = ResourceLoader.Load("res://Assets/LevelUI/Bg/bg2.png") as Texture;
            s.Texture = texture2;
        }

    }

    /// <summary>
    /// Load the required images for Character and Monster for the VS preview that plays before the level actually starts
    /// </summary>
    /// <param name="character"></param>
    /// <param name="monster"></param>
    public void LoadStart(Character character, Monster monster)
    {
        string charHead = spritePath + character.CharName + "/Head/head.png";
        string mobHead = spritePath + monster.MonsterName + "/Head/head.png";
        var texture1 = ResourceLoader.Load(charHead) as Texture;
        var texture2 = ResourceLoader.Load(mobHead) as Texture;
        Sprite charSprite = GetNode<Sprite>("Versus/Head1");
        Sprite mobSprite = GetNode<Sprite>("Versus/Head2");
        charSprite.Texture = texture1;
        mobSprite.Texture = texture2;

    }

    /// <summary>
    /// Handles the logic when the countdown animation finishes
    /// </summary>
    private void _on_Countdown_animation_finished()
    {
        //Unhide
        charSprite.Visible = true;
        monsterSprite.Visible = true;
        Label questionTitle = GetNode<Label>("QuestionTitle");
        questionTitle.Visible = true;
        levelTitle.Visible = true;
        Control ctr2 = GetNode<Control>("Buttons");
        ctr2.Visible = true;
        timerLabel.Visible = true;
        questionLabel.Visible = true;
        TextureButton skillBtn = GetNode<TextureButton>("SkillBtn");
        skillBtn.Visible = true;
        loadSkillIcon();
        //Hide
        Control versus = GetNode<Control>("Versus");
        versus.Visible = false;

        countDown.Visible = false;
        timer.Start();

    }

    /// <summary>
    /// Handles the logic when the Menu button is pressed
    /// </summary>
    private void _on_Menu_pressed()
    {
        AudioStream sfx = ResourceLoader.Load("res://Assets/SoundEffects/little town - orchestral.ogg") as AudioStream;
        DefaultSound.playSound(sfx);
        GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");

        switch (gameType)
        {
            case "Assignment":
                GetTree().ChangeScene("res://Presentation/Assignment/ViewAssignment.tscn");
                break;
            case "Campaign":
                GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
                break;
            case "CustomLevel":
                GetTree().ChangeScene("res://Presentation/CustomLevel/ViewCustomLevel.tscn");
                break;
        }

    }

    /// <summary>
    /// Handles the logic when the Next button is pressed
    /// </summary>
    private void _on_Next_pressed()
    {
        if (Global.LevelId != 5)
            Global.LevelId++;
        else
        {
            Global.SectionId++;
            Global.LevelId = 1;
        }
        AudioStream sfx = ResourceLoader.Load("res://Assets/SoundEffects/little town - orchestral.ogg") as AudioStream;
        DefaultSound.playSound(sfx);
        GetTree().ChangeScene("res://Presentation/GamePlay/Campaign.tscn");
    }

    /// <summary>
    /// Handles the logic when the Replay button is pressed
    /// </summary>
    private void _on_Replay_pressed()
    {
        switch (gameType)
        {
            case "Assignment":
                GetTree().ChangeScene("res://Presentation/Assignment/Assignment.tscn");
                break;
            case "Campaign":
                GetTree().ChangeScene("res://Presentation/GamePlay/Campaign.tscn");
                break;
            case "CustomLevel":
                GetTree().ChangeScene("res://Presentation/CustomLevel/CustomLevel.tscn");
                break;
        }

    }
}



