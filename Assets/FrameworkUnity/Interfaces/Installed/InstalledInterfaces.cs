namespace FrameworkUnity.Interfaces.Installed
{
    public interface IInstallable
    {
        void Install();
    }

    public interface IInstallableOnAwake : IInstallable { }
    public interface IInstallableOnStart : IInstallable { }
    public interface IInstallableOnEnable : IInstallable { }
    public interface IInstallableOnDisable : IInstallable { }
}
