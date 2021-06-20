namespace Meters.inGaia.Challenge.API.Models
{
    public class Property
    {
        public Property()
        {
        }

        public decimal PropertySizeInSquareMeters { get; private set; }

        public decimal Value { get; private set; }

        public string Error { get; private set; }

        public void SetPropertySize(decimal property)
        {
            PropertySizeInSquareMeters = property;
        }

        public void SetValue(decimal value)
        {
            Value = value;
        }

        public void SetError(string error)
        {
            Error = error;
        }
    }
}
