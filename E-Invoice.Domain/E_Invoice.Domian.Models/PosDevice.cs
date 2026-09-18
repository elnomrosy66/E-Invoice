using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Invoice.Domian.Models;

[Table("PosDevices")]
public class PosDevice : Base
{
	[Required]
	[MaxLength(100)]
	public string PosName { get; set; }

	[Required]
	[MaxLength(50)]
	public string PosCode { get; set; }

	[ForeignKey("BranchId")]
	public virtual Branch Branch { get; set; }

	[MaxLength(100)]
	public string DeviceSerialNumber { get; set; }

	[MaxLength(50)]
	public string DeviceOSVersion { get; set; }

	[MaxLength(50)]
	public string DeviceModel { get; set; }

	[MaxLength(50)]
	public string ActivityCode { get; set; }

	[MaxLength(150)]
	public string ClientId { get; set; }

	[MaxLength(150)]
	public string ClientSecret { get; set; }

	public long CurrentSequence { get; set; } = 1;

	[MaxLength(20)]
	public string ReceiptPrefix { get; set; } = "REC-";

	public bool IsProduction { get; set; } = false;
}
