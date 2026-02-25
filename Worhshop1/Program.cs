using System;

public class Time
{
    private int hour;
    private int minute;
    private int second;
    private int millisecond;

    public Time()
    {
        hour = 0;
        minute = 0;
        second = 0;
        millisecond = 0;
    }

    public Time(int h) : this(h, 0, 0, 0)
    {
    }

    public Time(int h, int m) : this(h, m, 0, 0)
    {
    }

    public Time(int h, int m, int s) : this(h, m, s, 0)
    {
    }

    public Time(int h, int m, int s, int ms)
    {
        if (h < 0 || h > 23)
            throw new ArgumentException("Invalid hour");

        if (m < 0 || m > 59)
            throw new ArgumentException("Invalid minute");

        if (s < 0 || s > 59)
            throw new ArgumentException("Invalid second");

        if (ms < 0 || ms > 999)
            throw new ArgumentException("Invalid millisecond");

        hour = h;
        minute = m;
        second = s;
        millisecond = ms;
    }

    public override string ToString()
    {
        int displayHour = hour;
        string period = "AM";

        if (hour >= 12)
        {
            period = "PM";
            if (hour > 12)
                displayHour = hour - 12;
        }

        if (displayHour == 0)
            displayHour = 12;

        return displayHour.ToString("00") + ":" +
               minute.ToString("00") + ":" +
               second.ToString("00") + "." +
               millisecond.ToString("000") + " " +
               period;
    }

    public long ToMilliseconds()
    {
        return (hour * 3600000L) +
               (minute * 60000L) +
               (second * 1000L) +
               millisecond;
    }

    public long ToSeconds()
    {
        return (hour * 3600L) +
               (minute * 60L) +
               second;
    }

    public long ToMinutes()
    {
        return (hour * 60L) + minute;
    }

    public bool IsOtherDay(Time other)
    {
        long totalMilliseconds = this.ToMilliseconds() + other.ToMilliseconds();
        long millisecondsInDay = 24L * 60L * 60L * 1000L;

        return totalMilliseconds >= millisecondsInDay;
    }

    public Time Add(Time other)
    {
        int newMillisecond = this.millisecond + other.millisecond;
        int extraSecond = newMillisecond / 1000;
        newMillisecond = newMillisecond % 1000;

        int newSecond = this.second + other.second + extraSecond;
        int extraMinute = newSecond / 60;
        newSecond = newSecond % 60;

        int newMinute = this.minute + other.minute + extraMinute;
        int extraHour = newMinute / 60;
        newMinute = newMinute % 60;

        int newHour = this.hour + other.hour + extraHour;
        newHour = newHour % 24;

        return new Time(newHour, newMinute, newSecond, newMillisecond);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Time t1 = new Time();
            Time t2 = new Time(10);
            Time t3 = new Time(10, 30);
            Time t4 = new Time(23, 58, 34, 666);
            Time t5 = new Time(5, 15, 45, 120);

            Time[] times = { t1, t2, t3, t4, t5 };

            foreach (Time t in times)
            {
                Console.WriteLine(t.ToString());
                Console.WriteLine(t.ToMilliseconds());
                Console.WriteLine(t.ToSeconds());
                Console.WriteLine(t.ToMinutes());
                Console.WriteLine();
            }

            foreach (Time t in times)
            {
                Time result = t.Add(t3);
                Console.WriteLine(t.ToString() + " + " + t3.ToString() + " = " + result.ToString());
            }

            Console.WriteLine();

            foreach (Time t in times)
            {
                Console.WriteLine(t.ToString() + " passes to next day: " + t.IsOtherDay(t4));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}
