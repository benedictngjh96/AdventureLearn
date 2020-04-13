using Godot;
using System;
using System.Collections.Generic;

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

    bool shieldOn = false;

    public override void _Ready()
    {
        AddUserSignal("NoMoreQuestions");

        charSprite = GetNode<AnimatedSprite>("CharSprite");
        monsterSprite = GetNode<AnimatedSprite>("MonsterSprite");
        charSkillSprite = GetNode<AnimatedSprite>("CharSprite/CharSkillSprite");
        monsterSkillSprite = GetNode<AnimatedSprite>("MonsterSprite/MonsterSkillSprite");
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
    public void SetLevelTitle(string title)
    {
        levelTitle.Text = title;
    }
    public void SetQuestionNum()
    {
        questionLabel.Text = String.Format("{0}/{1}", questionIndex + 1, questionList.Count);
    }
    public void SetTimeLimit(int timeLimit)
    {
        s = timeLimit;
        this.timeLimit = timeLimit;
    }
    public void SetQuestionList(List<Question> questionList)
    {
        this.questionList = questionList;
    }
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
                    correctAnsIndex = rng;

                optionList.RemoveAt(rng);
            }
        }
    }

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
    public void DisplayMonsterSprite(Monster monster)
    {
        string monsterPath = String.Format(spritePath + "{0}/", monster.MonsterName);
        Global.LoadSprite(monsterPath, monsterSprite, animationList);
    }
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
        }
    }
    private void HideBtns()
    {
        op1.Visible= false;
        op2.Visible = false;
        op3.Visible = false;
        op4.Visible = false;
    }
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
    private void _on_Win_animation_finished()
    {
        win.Stop();
        EmitSignal("NoMoreQuestions");
        s = 1000;
        popupMenu.Show();
    }
    private void _on_Lose_animation_finished()
    {
        s = 1000;
        lose.Stop();
        losePopup.Show();
    }
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
                shieldOn = false;
        }
        return result;
    }
    public int GetTimeLeft()
    {
        return s;
    }
    public void SetCharacter(Character character)
    {
        this.character = character;
    }
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
            timerLbl2.Visible = true;
            timer2.Start();
        }
    }

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
            timerLbl2.Visible = true;
            timer2.Start();
        }
    }
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
            timerLbl2.Visible = true;
            timer2.Start();
        }
    }

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
            timerLbl2.Visible = true;
            timer2.Start();
        }
    }

    private void _on_CharSprite_animation_finished()
    {
        if (charSprite.IsPlaying() && charSprite.Animation != "Idle" && charSprite.Animation != "Die")
            charSprite.Play("Idle");
    }

    private void _on_MonsterSprite_animation_finished()
    {
        if (monsterSprite.IsPlaying() && monsterSprite.Animation != "Idle" && monsterSprite.Animation != "Die")
            monsterSprite.Play("Idle");

    }
    private void _on_TimeLimit_timeout()
    {
        //s -= 1;
        ms -= 1;
    }
    private void _on_Timer_timeout()
    {
        //s -= 1;
        ms -= 1;
    }
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

    private void _on_CharSkillSprite_animation_finished()
    {
        if (!(charSkillSprite.Animation == "Shield"))
            charSkillSprite.Play("Default");
    }
    private void _on_MonsterSkillSprite_animation_finished()
    {
        monsterSkillSprite.Play("Default");
    }

    private void _on_SkillBtn_pressed()
    {
        switch (character.CharSkill)
        {
            case "Shield":
                charSkillSprite.Play("Shield");
                shieldOn = true;
                break;
            case "Remove Option":
                Random r = new Random();
                int random = 0;
                do
                {
                    random = r.Next(1, 5);
                } while (random == correctAnsIndex);
                AnimatedSprite effect = GetNode<AnimatedSprite>(String.Format("Buttons/Option{0}/Effect{0}", random));
                effect.Play("Explosion");
                string option = String.Format("Buttons/Option{0}", random);
                Button btn = GetNode<Button>(option);
                btn.Disabled = true;
                break;
        }
        Button skillBtn = GetNode<Button>("SkillBtn");
        skillBtn.Disabled = true;
    }
    private void _on_Effect1_animation_finished()
    {
        AnimatedSprite effect = GetNode<AnimatedSprite>("Buttons/Option1/Effect1");
        effect.Play("Default");
    }


    private void _on_Effect3_animation_finished()
    {
        AnimatedSprite effect = GetNode<AnimatedSprite>("Buttons/Option3/Effect3");
        effect.Play("Default");
    }


    private void _on_Effect2_animation_finished()
    {
        AnimatedSprite effect = GetNode<AnimatedSprite>("Buttons/Option2/Effect2");
        effect.Play("Default");
    }


    private void _on_Effect4_animation_finished()
    {
        AnimatedSprite effect = GetNode<AnimatedSprite>("Buttons/Option4/Effect4");
        effect.Play("Default");
    }
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
        Button skillBtn = GetNode<Button>("SkillBtn");
        skillBtn.Visible = true;

        //Hide
        Control versus = GetNode<Control>("Versus");
        versus.Visible = false;

        countDown.Visible = false;

        timer.Start();

    }
    private void _on_Menu_pressed()
    {
        GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
    }
    private void _on_Next_pressed()
    {
        if (Global.LevelId != 5)
            Global.LevelId++;
        else
        {
            Global.SectionId++;
            Global.LevelId = 1;
        }
        GetTree().ChangeScene("res://Presentation/GamePlay/Campaign.tscn");
    }
    private void _on_Replay_pressed()
    {
        GetTree().ChangeScene("res://Presentation/GamePlay/Campaign.tscn");
    }
}



