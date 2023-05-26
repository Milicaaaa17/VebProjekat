namespace ProjekatVeb2.Exceptions
{
    public class BadRequest : Exception
    {
        public BadRequest() { }
        public BadRequest(string poruka) : base(poruka) { }
    }
}
