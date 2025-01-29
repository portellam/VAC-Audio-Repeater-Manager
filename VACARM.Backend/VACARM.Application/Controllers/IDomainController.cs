namespace VACARM.Application.Controllers
{
  public interface IDomainController<T>
  {
    #region Logic

    IDomainController<T> Get();
    T Get(Func<T, bool> predicate);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetRange(Func<T, bool> predicate);
    void Restart(Func<T, bool> predicate);
    void RestartAll();
    void RestartRange(Func<T, bool> predicate);
    void Start(Func<T, bool> predicate);
    void StartAll();
    void StartRange(Func<T, bool> predicate);
    void Stop(Func<T, bool> predicate);
    void StopAll();
    void StopRange(Func<T, bool> predicate);
    void Update(Func<T, bool> predicate);
    void UpdateAll();
    void UpdateRange(Func<T, bool> predicate);

    #endregion
  }
}
