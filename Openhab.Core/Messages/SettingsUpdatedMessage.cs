﻿namespace OpenHAB.Core.Messages
{
    /// <summary>
    /// A message object that triggers a refresh of data after changing the settings.
    /// </summary>
    public class SettingsUpdatedMessage
    {
        /// <summary>Initializes a new instance of the <see cref="SettingsUpdatedMessage"/> class.</summary>
        /// <param name="isSettingsValid">if set to <c>true</c> [is settings valid].</param>
        public SettingsUpdatedMessage(bool isSettingsValid)
        {
            IsSettingsValid = isSettingsValid;
        }

        /// <summary>Gets a value indicating whether settings update valid.</summary>
        /// <value>
        ///   <c>true</c> if this settings changes are valid; otherwise, <c>false</c>.</value>
        public bool IsSettingsValid
        {
            get;
        }
    }
}
