using Godot;
using System;
using System.Collections.Generic;

/** Logic for a single stat requirement that can be imposed on a reply. */
public class ChatRequirement {
  private string _stat;
  private string _op;
  private double _threshold;

  public ChatRequirement(string reqText) {
    bool parsed = tryParse(reqText, "<");
    if (!parsed) {
      parsed = tryParse(reqText, ">");
    }
    if (!parsed) {
      GD.Print("Invalid requirement text: " + reqText);
      throw new System.ArgumentException("Invalid requirement text: " + reqText);
    }
  }

  public bool tryParse(string reqText, string op) {
    if (reqText.IndexOf(op) == -1) {
      return false;
    }
    string[] parts = reqText.split(op);
    if (parts.Length != 2) {
      return false;
    }
    _stat = parts[0].Trim();
    _op = op;
    _threshold = Double.Parse(parts[1].Trim());
	return true;
  }

  public bool isFulfilled(StatManager statManager) {
    switch (_op) {
      case "<":
        return statManager.GetStat(_stat) < _threshold;
      case ">":
        return statManager.GetStat(_stat) > _threshold;
    }
    GD.Print("Unsupported requirement operator: " + _op);
    return true;
  }
}
