
using System;

namespace Injector.Core.Settings
{
  /// <summary>
  /// Enumerator for a setting's scope (User/Global).
  /// </summary>
  public enum SettingScope
  {
    /// <summary>
    /// Global setting, doesn't allow per user/per plugin override.
    /// </summary>
    Global = 1,

    /// <summary>
    /// Per user setting: allows a different setting value per user.
    /// </summary>
    User = 2
  }

  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public sealed class SettingAttribute : Attribute
  {
    private SettingScope _settingScope;
    private object _defaultValue = null;
    private bool _hasDefault = false;

    /// <summary>
    /// Constructor which configures the scope of the annotated setting.
    /// </summary>
    /// <param name="settingScope">The scope the annotated setting should be contained in.</param>
    public SettingAttribute(SettingScope settingScope)
    {
      _settingScope = settingScope;
    }

    /// <summary>
    /// Constructor which configures the scope and the default value of the annotated setting.
    /// </summary>
    /// <param name="settingScope">The scope the annotated setting should be contained in.</param>
    /// <param name="defaultValue">Default value this setting will get if the value can not be
    /// loaded.</param>
    public SettingAttribute(SettingScope settingScope, object defaultValue) : this(settingScope)
    {
      _defaultValue = defaultValue;
      _hasDefault = true;
    }

    /// <summary>
    /// Get/Set the setting's scope (User/Global).
    /// </summary>
    public SettingScope SettingScope
    {
      get { return _settingScope; }
      set { _settingScope = value; }
    }

    /// <summary>
    /// Get/Set the setting's default value.
    /// </summary>
    public object DefaultValue
    {
      get { return _defaultValue; }
      set
      {
        _defaultValue = value;
        _hasDefault = true;
      }
    }

    /// <summary>
    /// Gets/sets the information if the configured <see cref="DefaultValue"/> will be used when no
    /// not-<c>null</c> value is available.
    /// </summary>
    public bool HasDefault
    {
      get { return _hasDefault; }
      set { _hasDefault = value; }
    }
  }
}
