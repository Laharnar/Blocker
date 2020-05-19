[System.Serializable]
public class ResponseToClick
{
    public int userId;
    public string context;
    internal UpgradeData data;
    public const int ACTIVEUSER = -2;
    public const UpgradeData EMPTYDATA = null;
}