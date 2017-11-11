using System;

public abstract class Singleton<T> where T : class
{
    private static T _instance;

    public static T GetInstance()
    {
        if (_instance == null)
            _instance = CreateInstanceOfT();
        return _instance;
    }

    private static T CreateInstanceOfT()
    {
        return Activator.CreateInstance(typeof(T), true) as T;
    }
}
