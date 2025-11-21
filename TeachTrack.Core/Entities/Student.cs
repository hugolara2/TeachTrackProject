using System;

namespace TeachTrack.Core.Entities;

public class Student {
    private int _enrollementNumber; 
    private string _name;
    private string _lastname;
    private int _score;

    public int EnrollmentNumber { 
        get => _enrollementNumber; 
        set => _enrollementNumber = value; 
    }

    public string Name {
        get => _name;
        set => _name = value;
    }

    public string Lastname {
        get => _lastname;
        set => _lastname = value;
    }

    public int Score {
        get => _score;
        set => _score = value;
    }

    public bool HasTheScore(int score, int necessaryScore) {
        if(score >= necessaryScore)
            return true;
        return false;
    }

}
