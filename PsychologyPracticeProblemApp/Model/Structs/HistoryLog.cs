﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.Model.Structs;
/// <summary>
/// Stores a single attempt at a problem.
/// </summary>
public class HistoryLog {
    private DataSet inputs;
    private double answer;
    private bool isCorrect;
    private IProblem problem;
    private DateTime date;
    public DataSet Inputs => inputs;
    public double Answer => answer;
    public bool IsCorrect => isCorrect;
    public IProblem Problem => problem;
    public DateTime Date => date;
    public HistoryLog(int type, DataSet inputs, double answer, DateTime date)
    {
        problem = IProblem.Problem[type];
        isCorrect = IProblem.ToPrecise(problem?.Solve(inputs)) == answer;
        this.inputs = inputs;
        this.answer = answer;
        this.date = date;
    }

}
