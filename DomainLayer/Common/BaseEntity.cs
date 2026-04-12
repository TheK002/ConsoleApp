namespace DomainLayer.Common
{
    public class BaseEntity
    {
        private static int _id;
        public int Id { get; set; }

        public BaseEntity()
        {
            Id = ++_id;
        }
    }
}