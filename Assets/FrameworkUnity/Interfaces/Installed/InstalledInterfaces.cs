namespace FrameworkUnity.Interfaces.Installed
{
    public interface IInstallable { }
    public interface IInstallableOnAwake : IInstallable
    {
        void InstallOnAwake();
    }
    public interface IInstallableOnStart : IInstallable
    {
        void InstallOnStart();
    }
    public interface IInstallableOnEnable : IInstallable
    {
        void InstallOnEnable();
    }
    public interface IInstallableOnDisable : IInstallable
    {
        void InstallOnDisable();
    }
}
