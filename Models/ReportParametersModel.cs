namespace Parqueadero.Models
{
    public class ReportParametersModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserId { get; set; }
        public IEnumerable<UsuariosModel> Users { get; set; }
        public dynamic ChartData { get; set; }
    }
}
