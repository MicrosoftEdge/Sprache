using System;
using System.Collections.Generic;

namespace Sprache.Core.Models
{
  /// <summary>
  /// Object for serialized language lookup
  /// </summary>
  public class LanguageLookup
  {
    /// <summary>
    /// The root language that will be mapped to
    /// </summary>
    public List<RootLanguage> RootLanguages { get; set; }
  }

  /// <summary>
  /// Defines the root language node
  /// </summary>
  public class RootLanguage
  {
    /// <summary>
    /// Root language code
    /// </summary>
    public String LanguageCode { get; set; }
    /// <summary>
    /// List of mapped languages
    /// </summary>
    public List<MappedLanguage> MappedLanguages { get; set; }
  }

  /// <summary>
  /// Defines Mapped Languages
  /// </summary>
  public class MappedLanguage
  {
    /// <summary>
    /// Mapped language code
    /// </summary>
    public String LanguageCode { get; set; }
  }
}
