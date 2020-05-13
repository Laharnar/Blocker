public class ExpGroups : ExpGroup
{
    static ExpGroups Singleton;
    public static void Connect(ExpCollector connector)
    {
        connector.Connect(Singleton);
    }
}
