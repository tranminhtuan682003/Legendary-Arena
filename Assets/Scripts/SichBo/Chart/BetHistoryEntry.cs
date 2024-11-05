// Lớp lưu trữ thông tin cho mỗi dòng lịch sử
public class BetHistoryEntry
{
    public string SessionId;       // Mã phiên chơi
    public string Date;            // Ngày đặt cược
    public int BetAmount;          // Số tiền cược
    public string IsWin;           // Trạng thái thắng/thua dưới dạng chuỗi
    public int MoneyChange;        // Số tiền thay đổi (âm hoặc dương)

    public BetHistoryEntry(string sessionId, string date, int betAmount, string isWin, int moneyChange)
    {
        SessionId = sessionId;
        Date = date;
        BetAmount = betAmount;
        IsWin = isWin;
        MoneyChange = moneyChange;
    }
}
