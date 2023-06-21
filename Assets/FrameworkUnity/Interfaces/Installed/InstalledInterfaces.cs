namespace FrameworkUnity.Interfaces.Installed
{
    public interface IBootstrapInstallable { }
    public interface IInstallableOnAwake : IBootstrapInstallable
    {
        void InstallOnAwake();
    }
    public interface IInstallableOnStart : IBootstrapInstallable
    {
        void InstallOnStart();
    }
    public interface IInstallableOnEnable : IBootstrapInstallable
    {
        void InstallOnEnable();
    }
    public interface IUninstallableOnDisable : IBootstrapInstallable
    {
        void UninstallOnDisable();
    }
    public interface IUninstallableOnDestroy : IBootstrapInstallable
    {
        void UninstallOnDestroy();
    }
}
