public static class Data
{
    private static int duration = 1;
    private static float chaserRate = 5f, shooterRate = 5f;

    public static int Duration
    {
        get
        {
            return duration;
        }
        set
        {
            duration = value;
        }
    }

    public static float ChaserRate
    {
        get
        {
            return chaserRate;
        }
        set
        {
            chaserRate = value;
        }
    }

    public static float ShooterRate
    {
        get
        {
            return shooterRate;
        }
        set
        {
            shooterRate = value;
        }
    }
}
