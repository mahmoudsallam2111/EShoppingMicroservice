namespace Catalog.api.Exceptions
{
    public class productNotFountException : Exception
    {
        public productNotFountException(Guid id) : base($"product with id : {id} is not found") 
        {
            
        }
    }
}
