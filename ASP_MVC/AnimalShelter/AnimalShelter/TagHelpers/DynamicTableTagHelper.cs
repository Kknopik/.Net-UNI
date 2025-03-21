using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AnimalShelter.TagHelpers
{
    public class DynamicTableTagHelper : TagHelper
    {
        public IEnumerable<string> Columns { get; set; }
        public IEnumerable<IEnumerable<string>> Rows { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Tag
            output.TagName = "table";
            output.Attributes.Add("class", "table table-striped table-bordered table-hover");

            // thead
            output.Content.AppendHtml("<thead><tr>");

            foreach (var column in Columns)
            {
                // Add column name
                output.Content.AppendHtml($"<th>{column}</th>");
            }

            output.Content.AppendHtml("</tr></thead><tbody>");

            // Rows
            foreach (var row in Rows)
            {
                output.Content.AppendHtml("<tr>");
                foreach (var cell in row)
                {
                    output.Content.AppendHtml($"<td>{cell}</td>");
                }
                output.Content.AppendHtml("</tr>");
            }

            output.Content.AppendHtml("</tbody>");
        }
    }
}
