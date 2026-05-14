/// <summary>
/// 업적을 연결할 interface
/// </summary>
/// 일일퀘 주간퀘 반복업적을 통합할 틀을 구성
public interface IQuestConnector {

    public void Connect();

    public void Reset();

    public void InitArray();
}
