[System.Serializable]
public class Net_OnCodeSending : NetMsg {
    
    public Net_OnCodeSending() {
        OP = NetOP.OnCodeSending;
    }

    public bool isFound { set; get;}
    public string Information { set; get;}
}
