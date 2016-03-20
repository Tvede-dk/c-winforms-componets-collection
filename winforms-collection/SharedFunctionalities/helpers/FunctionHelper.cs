using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class FunctionalHelpers {
    /// <summary>
    /// Maps a collection to another type of collection using a converter / generator
    /// </summary>
    /// <typeparam name="U">the result type</typeparam>
    /// <typeparam name="T">the input type</typeparam>
    /// <param name="input">the input list</param>
    /// <param name="generator">the generator / converter </param>
    /// <returns>the resulting collection</returns>
    public static List<U> map<U, T>(this IEnumerable<T> input, Func<T, U> generator) {
        List<U> result = new List<U>(input.Count());
        foreach (var item in input) {
            result.Add(generator(item));
        }
        return result;
    }

    public static void Foreach<T>(this IEnumerable<T> list, Action<T> onEach) {
        foreach (var item in list) {
            onEach(item);
        }
    }

    public static void PerformEachTime(this int times, Action<int> callback) {
        for (int i = 0; i < times; i++) {
            callback(i);
        }
    }
    /// <summary>
    /// Performs a task the given number of times unless the function returns true. 
    /// </summary>
    /// <param name="times"></param>
    /// <param name="callback"></param>
    /// <returns>the index we exited at (if no true returend it will be times -1 ).</returns>
    public static int PerformEachTimeUntil(this int times, Func<int, bool> callback) {
        for (int i = 0; i < times; i++) {
            if (callback(i) == true) {
                return i;
            }
        }
        return times - 1;
    }

    public static bool DoOnTrue(this bool val, Action ifTrue) {
        if (val) {
            ifTrue();
        }
        return val;
    }

    public static List<U> flattern<U>(this IEnumerable<IEnumerable<U>> collection) {
        var result = new List<U>(collection.Count());
        foreach (var item in collection) {
            result.AddRange(item);
        }
        return result;
    }

}
