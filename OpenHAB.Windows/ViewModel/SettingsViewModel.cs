using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.Logging;
using OpenHAB.Core.Common;
using OpenHAB.Core.Messages;
using Windows.ApplicationModel;

namespace OpenHAB.Windows.ViewModel
{
    /// <summary>
    /// Collects and formats all the data for user defined settings.
    /// </summary>
    public class SettingsViewModel : ViewModelBase<object>
    {
        private readonly ILogger<SettingsViewModel> _logger;
        private ConfigurationViewModel _configuration;
        private ActionCommand _saveCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel(ConfigurationViewModel configurationViewModel, ILogger<SettingsViewModel> logger)
            : base(new object())
        {
            _configuration = configurationViewModel;
            _configuration.PropertyChanged += Configuration_PropertyChanged;
            _logger = logger;
        }

        private void Configuration_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveCommand.InvokeCanExecuteChanged(null);
        }

        /// <summary>
        /// Gets the save command to persist the settings.
        /// </summary>
        /// <value>The save command.</value>
        public ActionCommand SaveCommand => _saveCommand ?? (_saveCommand = new ActionCommand(PersistSettings, CanPersistSettings));

        /// <summary>
        /// Gets or sets the current user-defined settings.
        /// </summary>
        public ConfigurationViewModel Settings
        {
            get => _configuration;
            set => Set(ref _configuration, value);
        }

        /// <summary>
        /// Gets the application version number.
        /// </summary>
        /// <value>The version number.</value>
        public string Version
        {
            get
            {
                Version version = new Version(
                    Package.Current.Id.Version.Major,
                    Package.Current.Id.Version.Minor,
                    Package.Current.Id.Version.Build,
                    Package.Current.Id.Version.Revision);

                return version.ToString();
            }
        }

        /// <summary>
        /// Save the user defined settings to the UWP settings storage.
        /// </summary>
        public void PersistSettings(object obj)
        {
            _logger.LogInformation("Execute save settings command");

            if (_configuration.IsConnectionConfigValid())
            {
                _configuration.Save();
            }

            Messenger.Default.Send(new SettingsUpdatedMessage(_configuration.IsConnectionConfigValid()));
        }

        private bool CanPersistSettings(object arg)
        {
            return _configuration.IsConnectionConfigValid() && _configuration.IsDirty;
        }
    }
}