using UnityEngine;
using CandyCoded.HapticFeedback;

public class Vibrator : MonoBehaviour
{
    public static void DefaultVibration() {
        Handheld.Vibrate();
    }

    public static void MediumVibration() {
        HapticFeedback.MediumFeedback();
    }
}
