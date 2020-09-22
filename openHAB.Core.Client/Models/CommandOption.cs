// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace openHAB.Core.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class CommandOption
    {
        /// <summary>
        /// Initializes a new instance of the CommandOption class.
        /// </summary>
        public CommandOption()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CommandOption class.
        /// </summary>
        public CommandOption(string command = default(string), string label = default(string))
        {
            Command = command;
            Label = label;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "command")]
        public string Command { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

    }
}