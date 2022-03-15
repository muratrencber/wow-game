public interface IDadItem
{
    string Key { get; }
    bool AvailableForConsumption { get; }
    void OnConsumption();
    void OnConsumptionFinish();
}
