// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace openHAB.Core.Client.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ChannelGroupDefinitionDTO
    {
        /// <summary>
        /// Initializes a new instance of the ChannelGroupDefinitionDTO class.
        /// </summary>
        public ChannelGroupDefinitionDTO()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ChannelGroupDefinitionDTO class.
        /// </summary>
        public ChannelGroupDefinitionDTO(string id = default(string), string description = default(string), string label = default(string), IList<ChannelDefinitionDTO> channels = default(IList<ChannelDefinitionDTO>))
        {
            Id = id;
            Description = description;
            Label = label;
            Channels = channels;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "channels")]
        public IList<ChannelDefinitionDTO> Channels { get; set; }

    }
}