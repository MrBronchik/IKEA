[System.Serializable]
public class Net_CodeSending : NetMsg {
    
    public Net_CodeSending() {
        OP = NetOP.CodeSending;
    }

    public string InstructionCode { set; get;}
}
