﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OpenHAB.Core.Contracts.Services;
using OpenHAB.Core.Model;
using OpenHAB.Core.Model.Connection;
using OpenHAB.Core.SDK;

namespace OpenHAB.Windows.ViewModel
{
    /// <summary>
    /// Class that holds all the OpenHAB Windows application settings.
    /// </summary>
    public class ConfigurationViewModel : ViewModelBase<object>
    {
        private readonly ILogger<ConfigurationViewModel> _logger;
        private readonly Settings _settings;
        private readonly ISettingsService _settingsService;
        private List<LanguageViewModel> _appLanguages;
        private bool? _isRunningInDemoMode;
        private ConnectionDialogViewModel _localConnection;
        private ConnectionDialogViewModel _remoteConnection;
        private LanguageViewModel _selectedAppLanguage;
        private bool _showDefaultSitemap;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationViewModel"/> class.
        /// </summary>
        public ConfigurationViewModel(ISettingsService settingsService, IOpenHAB openHabsdk, ILogger<ConfigurationViewModel> logger)
            : base(new object())
        {
            _settingsService = settingsService;
            _logger = logger;
            _settings = settingsService.Load();

            _localConnection = new ConnectionDialogViewModel(_settings.LocalConnection, openHabsdk, OpenHABHttpClientType.Local);
            _localConnection.PropertyChanged += ConnectionPropertyChanged;

            _remoteConnection = new ConnectionDialogViewModel(_settings.RemoteConnection, openHabsdk, OpenHABHttpClientType.Remote);
            _remoteConnection.PropertyChanged += ConnectionPropertyChanged;

            _isRunningInDemoMode = _settings.IsRunningInDemoMode;
            _showDefaultSitemap = _settings.ShowDefaultSitemap;

            _appLanguages = InitalizeAppLanguages();
            _selectedAppLanguage =
                _appLanguages.FirstOrDefault(x => string.Compare(x.Code, _settings.AppLanguage, StringComparison.InvariantCultureIgnoreCase) == 0);

            PropertyChanged += ConfigurationViewModel_PropertyChanged;
        }

        /// <summary>
        /// Gets or sets the supported application languages.
        /// </summary>
        /// <value>The application languages.</value>
        public List<LanguageViewModel> AppLanguages
        {
            get
            {
                return _appLanguages;
            }

            set
            {
                Set(ref _appLanguages, value);
            }
        }

        /// <summary>Gets a value indicating whether this instance is dirty.</summary>
        /// <value>
        ///   <c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
        public new bool IsDirty
        {
            get
            {
                return base.IsDirty || LocalConnection.IsDirty || RemoteConnection.IsDirty;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the app is currently running in demo mode.
        /// </summary>
        public bool? IsRunningInDemoMode
        {
            get
            {
                return _isRunningInDemoMode;
            }

            set
            {
                if (Set(ref _isRunningInDemoMode, value, true))
                {
                    _settings.IsRunningInDemoMode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets local OpenHAB connection configuration.
        /// </summary>
        public ConnectionDialogViewModel LocalConnection
        {
            get
            {
                return _localConnection;
            }

            set
            {
                Set(ref _localConnection, value);
            }
        }

        /// <summary>
        /// Gets or sets remote OpenHAB connection configuration.
        /// </summary>
        public ConnectionDialogViewModel RemoteConnection
        {
            get
            {
                return _remoteConnection;
            }

            set
            {
                Set(ref _remoteConnection, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected application language.
        /// </summary>
        /// <value>The selected application language.</value>
        public LanguageViewModel SelectedAppLanguage
        {
            get
            {
                return _selectedAppLanguage;
            }

            set
            {
                if (Set(ref _selectedAppLanguage, value, true))
                {
                    _settings.AppLanguage = value.Code;
                }
            }
        }

        /// <summary>,
        /// Gets or sets the if the default sitemap should be visible.
        /// </summary>
        /// <value>The hide default sitemap.</value>
        public bool ShowDefaultSitemap
        {
            get
            {
                return _showDefaultSitemap;
            }

            set
            {
                if (Set(ref _showDefaultSitemap, value, true))
                {
                    _settings.ShowDefaultSitemap = value;
                }
            }
        }

        /// <summary>Determines whether [is connection configuration valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is connection configuration valid]; otherwise, <c>false</c>.</returns>
        public bool IsConnectionConfigValid()
        {
            bool validConfig = IsRunningInDemoMode.Value ||
                     !string.IsNullOrEmpty(LocalConnection?.Url) ||
                     !string.IsNullOrEmpty(RemoteConnection?.Url);

            _logger.LogInformation($"Valid application configuration: {validConfig}");

            return validConfig;
        }

        /// <summary>
        /// Persists the settings to disk.
        /// </summary>
        public void Save()
        {
            _settings.LocalConnection = _localConnection.Model;
            _settings.RemoteConnection = _remoteConnection.Model;

            _settingsService.Save(_settings);
            _settingsService.SetProgramLanguage(null);
        }

        private void ConfigurationViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IsDirty))
            {
                OnPropertyChanged(nameof(IsDirty));
            }
        }

        private void ConnectionPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsDirty));
        }

        private List<LanguageViewModel> InitalizeAppLanguages()
        {
            List<LanguageViewModel> appLanguages = new List<LanguageViewModel>();
            LanguageViewModel system = new LanguageViewModel()
            {
                Name = "System",
                Code = null
            };

            LanguageViewModel english = new LanguageViewModel()
            {
                Name = "English",
                Code = "en-us"
            };

            LanguageViewModel german = new LanguageViewModel()
            {
                Name = "Deutsch",
                Code = "de-de"
            };

            appLanguages.Add(system);
            appLanguages.Add(english);
            appLanguages.Add(german);

            return appLanguages;
        }
    }
}
