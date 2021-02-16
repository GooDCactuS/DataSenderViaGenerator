using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSenderViaGenerator.Models
{
    public class DataField
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }

        public SelectList Types { get; set; }
    }

    public enum FieldType
    {
        Integer,
        String,
        Date,
        Guid,
        Email,
        Phone
    }
}
