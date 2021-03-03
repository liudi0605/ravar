using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string StartMessage { get; set; }

    public Action<Monster> OnStart { get; set; }
    public Func<Monster, bool> OnTurnStart { get; set; }
    public Action<Monster> OnTurnEnd { get; set; }
}