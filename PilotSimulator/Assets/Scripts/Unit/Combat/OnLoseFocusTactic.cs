public static class OnLoseFocusTactic
{
    public static void Do(CombatController controller)
    {
        controller.used = false;
        controller.StopWalking();
    }
}