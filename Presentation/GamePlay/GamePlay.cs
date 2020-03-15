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
    Character character;
    Question question;
    Label timerLabel;
    Label questionLabel;
    Label levelTitle;
    Button option1;
    Button option2;
    Button option3;
    Button option4;

    Timer timer;

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
        option1 = GetNode<Button>("Buttons/Option1");
        option2 = GetNode<Button>("Buttons/Option2");
        option3 = GetNode<Button>("Buttons/Option3");
        option4 = GetNode<Button>("Buttons/Option4");
        win = GetNode<AnimatedSprite>("Win");
        lose = GetNode<AnimatedSprite>("Lose");
        animationList = new List<string>();
        animationList.Add("Idle");
        animationList.Add("Hurt");
        animationList.Add("Die");
        animationList.Add("Attack");

        countDown.Play("Countdown");
        timer = GetNode<Timer>("TimeLimit");
    }

    public override void _Process(float delta)
    {

        if (s <= 0)
        {
            //GetTree().ChangeScene("res://Presentation/World/World.tscn");
            charSprite.Play("Die");
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
                Button optionButton = n as Button;
                int rng = r.Next(optionList.Count);

                optionButton.Text = optionList[rng];
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
        if (character.CharName == "Warrior2")
            charSprite.Position = new Vector2(351, 289);
        else if (character.CharName == "Zeus")
            charSprite.Position = new Vector2(355, 289);
        else if (character.CharName == "Knight1")
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
        foreach (Node btn in ctr.GetChildren())
        {
            (btn as Button).Disabled = false;
        }
        if (questionIndex < questionList.Count - 1)
        {
            questionIndex++;
            DisplayQuestion();
            SetQuestionNum();
        }
        else
        {
            //EmitSignal("NoMoreQuestions");
            timerLabel.Visible = false;
            monsterSprite.Play("Die");
            win.Play("Win");
        }
    }
    private void _on_Win_animation_finished()
    {
        EmitSignal("NoMoreQuestions");
        GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
    }
    private void _on_Lose_animation_finished()
    {
        GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
    }
    public void CheckCorrectAnswer(string option)
    {
        Random r = new Random();
        int random = r.Next(1, 4);
        if (option == question.CorrectOption)
        {
            charSprite.Play("Attack");
            monsterSprite.Play("Hurt");
            monsterSkillSprite.Play(String.Format("Explosion{0}", random));
            DisplayNextQuestion();
        }
        else
        {
            monsterSprite.Play("Attack");
            charSkillSprite.Play(String.Format("Explosion{0}", random));
            if (!shieldOn)
            {
                charSprite.Play("Hurt");
                s -= 10;
            }
            else
                shieldOn = false;
        }
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
        CheckCorrectAnswer(option1.Text);
    }

    private void _on_Option2_pressed()
    {
        CheckCorrectAnswer(option2.Text);
    }

    private void _on_Option3_pressed()
    {
        CheckCorrectAnswer(option3.Text);
    }

    private void _on_Option4_pressed()
    {
        CheckCorrectAnswer(option4.Text);
    }

    private void _on_CharSprite_animation_finished()
    {
        if (charSprite.IsPlaying() && charSprite.Animation != "Idle" && charSprite.Animation !="Die")
            charSprite.Play("Idle");
    }

    private void _on_MonsterSprite_animation_finished()
    {
        if (monsterSprite.IsPlaying() && monsterSprite.Animation != "Idle" && monsterSprite.Animation !="Die")
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
        Random r = new Random();
        int rng = r.Next(1, 4);
        var texture2 = ResourceLoader.Load(String.Format("res://Assets/LevelUI/Bg/bg{0}.png", rng)) as Texture;
        s.Texture = texture2;
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
        Control ctr = GetNode<Control>("Buttons");
        ctr.Visible = true;
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
}



