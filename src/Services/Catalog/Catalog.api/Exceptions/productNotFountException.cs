using BuildingBlocks.Exceptions;

namespace Catalog.api.Exceptions
{
    public class productNotFountException : NotFoundException
    {
        public productNotFountException(Guid id) : base("product" , id) 
        {
            
        }
    }
}
