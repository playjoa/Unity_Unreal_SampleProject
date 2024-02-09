using UnityEngine;

namespace Utils.Extensions
{
    public static class NumbersExtensions
    {
        public static int SecondsToMilliseconds(this int seconds) => seconds * 1000;
        
        public static string ToNiceCurrency(this int value) => value.ToString("N0");
        
        public static string ToNiceTimer(this float timerCount)
        {
            var minutes = Mathf.FloorToInt(timerCount / 60f);
            var seconds = Mathf.FloorToInt(timerCount - minutes * 60f);
            return $"{minutes:0}:{seconds:00}"; 
        }
        
        public static string FloatToPercentage(this float progress) => (progress * 100f).ToString("F2") + "%";
    }
}