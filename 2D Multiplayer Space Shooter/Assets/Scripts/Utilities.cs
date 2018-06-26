using System;
using System.Linq;

public static class Utilities {
	private static Random _random = new Random();

    public static string RandomCode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
    }
  
}

