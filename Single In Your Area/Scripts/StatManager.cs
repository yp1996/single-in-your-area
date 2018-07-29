using Godot;
using System;
using System.Collections.Generic;

public class StatManager : Node
{
    public class Stat {
		public String name;
		public int minVal;
		public int maxVal;
		public int currentVal;
		
		public Stat(String nm, int mv, int maxv, int defaultV) {
			name = nm;
			minVal = mv;
			maxVal = maxv;
			currentVal = defaultV;
		}
	}
	
	private Dictionary<String, Stat> stats = new Dictionary<String, Stat>();

    public override void _Ready()
    {
        Stat anxiety = new Stat("anxiety", 0, 10000, -50);
		stats.Add("anxiety", anxiety);   
		
	    Stat health = new Stat("health", 0, 100, 100);
		stats.Add("health", health);
		
		Stat distance = new Stat("emotional_distance", 0, 100, 50);
		stats.Add("emotional_distance", distance);
	          
    }
	
	public int GetStat(string statName) {
		Stat stat;
		if (stats.TryGetValue(statName, out stat)) // Returns true.
        {
            return stat.currentVal; 
        } else {
			return -666;
		}
	}
	
	public float GetRelativeStat(string statName) {
		Stat stat;
		if (stats.TryGetValue(statName, out stat)) // Returns true.
        {
            return (float) ((float) stat.currentVal / (float) stat.maxVal); 
        } else {
			return -666;
		}
	}
	
	public void IncrementStat(string statName, int increment=1) {
		Stat stat;
		if (stats.TryGetValue(statName, out stat)) // Returns true.
        {
            stat.currentVal = Mathf.Max(Mathf.Min(stat.currentVal + increment, stat.maxVal), stat.minVal);
        } 
	}
	
	public void SetStat(string statName, int value) {
		Stat stat;
		if (stats.TryGetValue(statName, out stat)) // Returns true.
        {
            stat.currentVal = Mathf.Max(Mathf.Min(value, stat.maxVal), stat.minVal);
        } 
	}
	
	public bool IsStatMaxed(string statName, int value) {
		Stat stat;
		if (stats.TryGetValue(statName, out stat)) // Returns true.
	    {
	        return (stat.currentVal == stat.maxVal);
	    } else {
			return false;
		}
	}

}
