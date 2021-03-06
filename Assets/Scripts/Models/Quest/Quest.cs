﻿#region License
// ====================================================
// Project Porcupine Copyright(C) 2016 Team Porcupine
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion
using System.Collections.Generic;
using System.Xml;
using MoonSharp.Interpreter;

[MoonSharpUserData]
public class Quest
{
    public string Name;
    public string Description;

    public List<QuestGoal> Goals;

    public bool IsAccepted;
    public bool IsCompleted;

    public List<QuestReward> Rewards;

    public List<string> PreRequiredCompletedQuest;

    public void ReadXmlPrototype(XmlTextReader reader_parent)
    {
        Name = reader_parent.GetAttribute("Name");
        Goals = new List<QuestGoal>();
        Rewards = new List<QuestReward>();   
        PreRequiredCompletedQuest = new List<string>();

        XmlReader reader = reader_parent.ReadSubtree();

        while (reader.Read())
        {
            switch (reader.Name)
            {
                case "Description":
                    reader.Read();
                    Description = reader.ReadContentAsString();
                    break;
                case "PreRequiredCompletedQuests":
                    XmlReader subReader = reader.ReadSubtree();

                    while (subReader.Read())
                    {
                        if (subReader.Name == "PreRequiredCompletedQuest")
                        {
                            PreRequiredCompletedQuest.Add(subReader.GetAttribute("Name"));
                        }
                    }

                    break;
                case "Goals":
                    XmlReader goals_reader = reader.ReadSubtree();
                    while (goals_reader.Read())
                    {
                        if (goals_reader.Name == "Goal")
                        {
                            QuestGoal goal = new QuestGoal();
                            goal.ReadXmlPrototype(goals_reader);
                            Goals.Add(goal);
                        }
                    }

                    break;
                case "Rewards": 
                    XmlReader reward_reader = reader.ReadSubtree();
                    while (reward_reader.Read())
                    {
                        if (reward_reader.Name == "Reward")
                        {
                            QuestReward reward = new QuestReward();
                            reward.ReadXmlPrototype(reward_reader);
                            Rewards.Add(reward);
                        }
                    }

                    break;
            }
        }
    }
}