using Godot;
using System;

public class Chart : Node
{
    ReferenceRect chart;
    public override void _Ready()
    {
        chart =GetNode<ReferenceRect>("chart");


    }


}
