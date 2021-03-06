﻿using System;
using System.Collections.Generic;
using System.Linq;

public static class TextPrediction {

    private static readonly HashSet<string> _toplevelDomains = new HashSet<string>();

    private static readonly StrCompare _comparrer = new StrCompare();

    static TextPrediction() {
        _toplevelDomains = new HashSet<string>(GetToplevelDomains(), _comparrer);
    }

    public static List<string> GetToplevelDomains() {
        return SharedFunctionalities.Properties.Resources.topleveldomains.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    /// <summary>
    /// Tells if this string ends in a topdomain (propperly a homepage). Just a heuristic.
    /// </summary>
    /// <param name="str"></param>
    /// <returns>true if belived to be a top leve domain[time is approx less than 1 ms] </returns>
    public static bool EndsWithToplevelDomain(this string str) {
        var result = false;
        var strend = str.LastIndexOf(".");
        if (strend != -1 && str.Length > strend + 1) {
            var possibleLevel = str.Substring(strend + 1);
            result = _toplevelDomains.Contains(possibleLevel, _comparrer);
        }
        return result;
    }

    private class StrCompare : IEqualityComparer<string> {
        public bool Equals(string x, string y) {
            return x.Equals(y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj) {
            return obj.GetHashCode();
        }
    }
}
