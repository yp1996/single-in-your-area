using Godot;
using System;
using System.Collections.Generic;

public class Requirement 
{

    public class RequirementCondition {
        public string statName;
	    public int reqValue;
        public enum comparison {Lesser, Greater, Equal};
        public comparison comparisonType;

        public RequirementCondition(string statName="anxiety", int value=0, comparison comp = comparison.Greater) {
            this.statName = statName;
            this.reqValue = value;
            this.comparisonType = comp;
        }

        public bool isFulfilled(int value) {
            GD.Print(value);
            switch (comparisonType) {
                case comparison.Greater:
                    return value > reqValue;
                case comparison.Lesser:
                    return value < reqValue;
                case comparison.Equal:
                    return value == reqValue;
                default:
                    return false;
            }
        }
    }
    List<RequirementCondition> requirements;
	
    public Requirement() {
        requirements = new List<RequirementCondition>();
	}

    public Requirement addCondition(string name="anxiety", int value=-1, RequirementCondition.comparison comp = RequirementCondition.comparison.Greater) {
        this.requirements.Add(new RequirementCondition(name, value, comp));
        return this;
    }
	
	public bool isFulfilled(StatManager statManager) {
        foreach (RequirementCondition cond in requirements) {
            GD.Print(cond.statName);
            GD.Print(cond.reqValue);
            if (!cond.isFulfilled(statManager.GetStat(cond.statName))) {
                return false;
            }
        }
        return true;
    }
    
}
