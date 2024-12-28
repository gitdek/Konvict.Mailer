using System;

namespace Injector.Core.Settings
{
  /// <summary>
  /// Default <see cref="ISettingsManager"/> implementation that does absolutely nothing
  /// </summary>
  /// <remarks>
  /// </remarks>
  internal class NoSettingsManager : ISettingsManager
  {
    #region ISettingsManager Members

    public SettingsType Load<SettingsType>() where SettingsType : class
    {
      return (SettingsType) Load(typeof(SettingsType));
    }

    public object Load(Type settingsType)
    {
      return Activator.CreateInstance(settingsType);
    }

    public void Save(object settingsObject) { }

    public void StartBatchUpdate() { }

    public void EndBatchUpdate() { }

    public void CancelBatchUpdate() { }

    public void RemoveSettingsData(Type settingsType, bool user, bool global) { }

    public void RemoveAllSettingsData(bool user, bool global) { }

    #endregion
  }
}
