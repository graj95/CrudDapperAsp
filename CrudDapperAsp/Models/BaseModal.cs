namespace CrudDapperAsp.Models
{
    public class BaseModal
    {
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
      
    }
}
