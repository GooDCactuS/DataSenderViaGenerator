using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSenderViaGenerator.Models
{
    public class GeneratorSettings
    {
        [BindProperty]
        public IList<DataField> Fields { get; set; }

        [BindProperty]
        public List<string> Types { get; set; }

        [BindProperty]
        public int RowNumber { get; set; }

        [BindProperty]
        public int StartIndex { get; set; }

        public GeneratorSettings()
        {
            GenerateFields();
            GenerateTypes();

            RowNumber = 1000;
            StartIndex = 0;
        }

        private void GenerateFields()
        {
            Fields = new List<DataField>
            {
                new DataField { Name = "Id", Type = FieldType.Integer },
                new DataField { Name = "Name", Type = FieldType.String },
                new DataField { Name = "Email", Type = FieldType.Email },
                new DataField { Name = "Phone", Type = FieldType.Phone },
                new DataField { Name = "Guid", Type = FieldType.Guid },
                new DataField { Name = "Date", Type = FieldType.Date }
            };
        }

        private void GenerateTypes()
        {
            Types = Enum.GetNames(typeof(FieldType)).ToList();
        }
    }
}
