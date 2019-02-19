/// <summary>
/// Interface identifying objects detectable by IDetecters.
/// </summary>
public interface IDetectable {
    void OnEnter(IDetecter _detecter);
    void OnExit(IDetecter _detecter);
}
