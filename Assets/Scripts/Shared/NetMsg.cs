public static class NetOP {
    public const int None = 0;

    // From Client
    public const int CreateAccount = 1;
    public const int CodeSending = 2;

    // From Server
    public const int OnCodeSending = 3;
}

[System.Serializable]
public abstract class NetMsg {
    public byte OP { set; get;}

    public NetMsg() {
        OP = NetOP.None;
    }
}
