﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace OpenHAB.Core.Model
{
    /// <summary>
    /// A class that represents an OpenHAB widget.
    /// </summary>
    public class OpenHABWidget : INotifyPropertyChanged
    {
        private string _icon;
        private string _label;

        /// <summary>Occurs when a property value changes.</summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHABWidget"/> class.
        /// </summary>
        public OpenHABWidget()
        {
            Children = new List<OpenHABWidget>();
        }

        /// <summary>
        /// Gets or sets the ID of the OpenHAB widget.
        /// </summary>
        public string Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Label of the OpenHAB widget.
        /// </summary>
        public string Label
        {
            get => _label;

            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                {
                    _label = value;
                    return;
                }

                var parts = value.Split(new[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                _label = parts[0];

                if (parts.Length > 1)
                {
                    Value = parts[1];
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Value of the widget.
        /// </summary>
        public string Value
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Icon of the OpenHAB widget.
        /// </summary>
        public string Icon
        {
            get => _icon ?? string.Empty;
            set => _icon = value;
        }

        /// <summary>
        /// Gets or sets the Type of the OpenHAB widget.
        /// </summary>
        public string Type
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Url of the OpenHAB widget.
        /// </summary>
        public string Url
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Period of the OpenHAB widget.
        /// </summary>
        public string Period
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Service of the OpenHAB widget.
        /// </summary>
        public string Service
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the MinValue of the OpenHAB widget.
        /// </summary>
        public float MinValue
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the MaxValue of the OpenHAB widget.
        /// </summary>
        public float MaxValue
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Step of the OpenHAB widget.
        /// </summary>
        public float Step
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Refresh of the OpenHAB widget.
        /// </summary>
        public int Refresh
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Height of the OpenHAB widget.
        /// </summary>
        public int Height
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the State of the OpenHAB widget.
        /// </summary>
        public string State
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the IconColor of the OpenHAB widget.
        /// </summary>
        public string IconColor
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the LabelColor of the OpenHAB widget.
        /// </summary>
        public string LabelColor
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the ValueColor of the OpenHAB widget.
        /// </summary>
        public string ValueColor
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Encoding of the OpenHAB widget.
        /// </summary>
        public string Encoding
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Item of the OpenHAB widget.
        /// </summary>
        public OpenHABItem Item
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Parent of the OpenHAB widget.
        /// </summary>
        public OpenHABWidget Parent
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Children of the OpenHAB widget.
        /// </summary>
        [JsonProperty(PropertyName = "widgets")]
        public ICollection<OpenHABWidget> Children
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Mapping of the OpenHAB widget.
        /// </summary>
        public ICollection<OpenHABWidgetMapping> Mappings
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the linked page when available.
        /// </summary>
        public OpenHABSitemap LinkedPage
        {
            get; set;
        }
    }
}
