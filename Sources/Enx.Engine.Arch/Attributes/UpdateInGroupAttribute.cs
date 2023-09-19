namespace Enx.Engine.Arch;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class UpdateInGroupAttribute<TGroup> : UpdateInGroupAttribute
    where TGroup : SystemGroup
{

}

public abstract class UpdateInGroupAttribute : Attribute
{
    public bool OrderFirst;
    public bool OrderLast;

    public int Value
    {
        get
        {
            if (OrderFirst) return 2;
            else if (OrderLast) return 0;
            else return 1;
        }
    }
}
