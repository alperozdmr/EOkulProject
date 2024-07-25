namespace Core.Entities.Concrete
{
    public class StudentOperationClaim : IEntitiy
    {

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
