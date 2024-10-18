using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CatalagoProduto.APIs.Models
{
    public class DateOnlyBrazilianConverter : JsonConverter<DateOnly>
    {
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                throw new JsonException("Valor nulo não permitido para DateOnly");
            }

            var dateString = reader.GetString();
            return DateOnly.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
                ? date
                : throw new JsonException("Formato de data inválido");
        }
    }

}
