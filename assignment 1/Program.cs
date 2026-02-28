namespace bakent;

public class Time
{




    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    // Constructor with no parameters
    public Time()
    {
        _hour = 0;
        _minute = 0;
        _second = 0;
        _millisecond = 0;
    }

    // Constructor with hours
    public Time(int hour)
    {
        Hour = hour;
    }

    // Constructor with hours and minutes
    public Time(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
    }

    // Constructor with hours, minutes and seconds
    public Time(int hour, int minute, int second)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    // Constructor with hours, minutes, seconds and milliseconds
    public Time(int hour, int minute, int second, int milisecond)
    {

        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = milisecond;
    }

    public int Hour
    {
        get => _hour;
        set => _hour = ValidHour(value);

    }

    public int Minute
    {
        get => _minute;
        set => _minute = ValidMinute(value);
    }
    public int Second
    {
        get => _second;
        set => _second = ValidSecond(value);

    }

    public int Millisecond
    {
        get => _millisecond;
        set => _millisecond = ValidMillisecond(value);
    }
    // Returns time in AM/PM format
    public override string ToString()
    {
        if (Hour < 0 || Hour > 23)
            throw new ArgumentOutOfRangeException("Ithe time entered is not valid");

        if (Minute < 0 || Minute > 59)
            throw new ArgumentOutOfRangeException("Ithe time entered is not valid");

        if (Second < 0 || Second > 59)
            throw new ArgumentOutOfRangeException("Ithe time entered is not valid");

        if (Millisecond < 0 || Millisecond > 999)
            throw new ArgumentOutOfRangeException("Ithe time entered is not valid");
        int displayHour = Hour;
        string period = "AM";

        if (Hour >= 12)
        {
            period = "PM";
            if (Hour > 12)
                displayHour = Hour - 12;
        }

        if (displayHour == 0)
            displayHour = 12;

        return $"{displayHour:00}:{Minute:00}:{Second:00}.{Millisecond:000} {period}";
    }

    // Returns total milliseconds
    public int ToMilliseconds()
    {

        if (Hour < 0 || Hour > 23)
            return 0;

        if (Minute < 0 || Minute > 59)
            return 0;

        if (Second < 0 || Second > 59)
            return 0;

        if (Millisecond < 0 || Millisecond > 999)
            return 0;
        return (Hour * 3600000) +
               (Minute * 60000) +
               (Second * 1000) +
               Millisecond;
    }

    // Returns total seconds
    public int ToSeconds()
    {

        if (Hour < 0 || Hour > 23)
            return 0;

        if (Minute < 0 || Minute > 59)
            return 0;

        if (Second < 0 || Second > 59)
            return 0;

        if (Millisecond < 0 || Millisecond > 999)
            return 0;
        return (Hour * 3600) +
               (Minute * 60) +
               Second +
               (Millisecond / 1000);
    }

    // Returns total minutes
    public int ToMinutes()
    {

        if (Hour < 0 || Hour > 23)
            return 0;

        if (Minute < 0 || Minute > 59)
            return 0;

        if (Second < 0 || Second > 59)
            return 0;

        if (Millisecond < 0 || Millisecond > 999)
            return 0;
        return (Hour * 60) + Minute + (Second / 60) + (Millisecond / 60000);
    }

    // Checks if adding another Time passes to next day
    public bool IsOtherDay(Time other)
    {
        int totalMilliseconds = this.ToMilliseconds() + other.ToMilliseconds();
        if (totalMilliseconds >= 86400000)
        {
            return true;
        }

        return false;
    }


    // Adds two Time objects
    public Time Add(Time other)
    {
        int TotalMillisecond = this.ToMilliseconds() + other.ToMilliseconds();

        TotalMillisecond %= 86400000;
        int newHour = (TotalMillisecond / 3600000);
        int newMinute = (TotalMillisecond / 60000) % 60;
        int newSecond = (TotalMillisecond / 1000) % 60;
        int newMillisecond = TotalMillisecond % 1000;

        return new Time(newHour, newMinute, newSecond, newMillisecond);
    }
    private int ValidHour(int hour)
    {
        if (hour < 0 || hour > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(hour), $"The hour: {hour}, is not valid.");
        }
        return hour;
    }


    private int ValidMinute(int minute)
    {
        if (minute < 0 || minute > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(minute), $"The minute: {minute}, is not valid.");
        }
        return minute;
    }
    private int ValidSecond(int second)
    {
        if (second < 0 || second > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(second), $"The second: {second}, is not valid.");
        }
        return second;
    }
    private int ValidMillisecond(int millisecond)
    {
        if (millisecond < 0 || millisecond > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(millisecond), $"The millisecond: {millisecond}, is not valid.");
        }
        return millisecond;
    }
}
