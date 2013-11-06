using System;
using System.Configuration;

namespace Sprache.Core.Support
{
  /// <summary>
  /// Custom configuration section definition
  /// </summary>
  public class SpracheConfiguration : ConfigurationSection
  {
    /// <summary>
    /// Points to the location of the Language Lookup xml file.  Relative to project directory.
    /// </summary>

    [ConfigurationProperty("languageLookupSource", DefaultValue = "", IsRequired = false)]
    public String LanguageLookupSource
    {
      get { return (string)this["languageLookupSource"]; }
    }
  }
}
