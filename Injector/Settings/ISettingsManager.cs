using System;

namespace Injector.Core.Settings
{
  /// <summary>
  /// Global service interface for loading and saving settings for application modules.
  /// The settings manager provides methods to load and save module specific settings objects.
  /// </summary>
  /// <remarks>
  /// The implementation of this interface defines where the setting object will be stored and
  /// how it will be serialized/deserialized.
  /// Every application part, which needs a setting, will define its own settings class to
  /// contain the settings values. Those classes will use the <see cref="Setting"/> meta
  /// attribute to define if a settings entry will be stored as global or user setting.
  /// There must be at most one instance of every settings class for holding application settings;
  /// a settings class must not be reused for different application settings of a similar
  /// type. The settings system will use the class name to find a settings object in the settings
  /// store.
  /// TODO: Document settings structure: [Setting] meta attribute, supported types, etc.
  /// </remarks>
  public interface ISettingsManager
  {
    /// <summary>
    /// Retrieves an object's public properties from the application's settings store.
    /// This is a convenience method for <see cref="Load(Type)"/>.
    /// </summary>
    /// <typeparam name="SettingsType">Type of the settings object to load.</typeparam>
    /// <returns>Application settings of the specified <typeparamref name="SettingsType"/>, if
    /// present. Else, returns an empty instance of that type.</returns>
    SettingsType Load<SettingsType>() where SettingsType: class;

    /// <summary>
    /// Retrieves an object's public properties from the application's settings store.
    /// </summary>
    /// <param name="settingsType">Type of settings to load.</param>
    /// <returns>Application settings of the specified <paramref name="settingsType"/>, if
    /// present. Else, returns an empty instance of that type.</returns>
    object Load(Type settingsType);

    /// <summary>
    /// Stores an object's public properties in the application's settings store.
    /// </summary>
    /// <exception cref="ArgumentNullException">If the specified <paramref name="settingsObject"/>
    /// is null.</exception>
    /// <param name="settingsObject">Settings object's instance to be saved.</param>
    void Save(object settingsObject);

    /// <summary>
    /// Switches on the batch update mode. In batch update mode, the settings manager neither won't dispose
    /// loaded settings objects in its cache, nor will it write settings objects to disk.
    /// To stop the batch update mode, call <see cref="EndBatchUpdate"/> or <see cref="CancelBatchUpdate"/>.
    /// </summary>
    void StartBatchUpdate();

    /// <summary>
    /// Leaves the batch update mode. Any settings for which method <see cref="Save"/> was called in
    /// batch update mode will be saved to disk.
    /// </summary>
    void EndBatchUpdate();

    /// <summary>
    /// Leaves the batch update mode. Any changes to settings in the batch update mode will be lost.
    /// </summary>
    void CancelBatchUpdate();

    /// <summary>
    /// Removes the application setting with the specified <paramref name="settingsType"/> from the system.
    /// </summary>
    /// <param name="settingsType">Type of settings to remove.</param>
    /// <param name="user">If set to <c>true</c>, the user data will be removed for the specified setting.</param>
    /// <param name="global">If set to <c>true</c>, the global data will be removed for the specified setting.</param>
    void RemoveSettingsData(Type settingsType, bool user, bool global);

    /// <summary>
    /// Removes all application configuration data from the system.
    /// </summary>
    /// <param name="user">If set to <c>true</c>, all user data will be removed.</param>
    /// <param name="global">If set to <c>true</c>, all global data will be removed.</param>
    void RemoveAllSettingsData(bool user, bool global);
  }
}
