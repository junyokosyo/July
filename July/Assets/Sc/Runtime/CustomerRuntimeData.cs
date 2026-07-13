/// <summary>
/// カスタマーのランタイムデータを保持するクラス
/// </summary>
public class CustomerRuntimeData
{
    public CustomerData Template;
    public WeaponData ResolvedItem;

    public CustomerRuntimeData(CustomerData data)
    {
        Template = data;
        ResolvedItem = RandomPicker.PickRandom(data.Weapons);
    }
}