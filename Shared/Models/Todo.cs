using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
	public sealed class Todo
	{
		[Key]
		[Required]
		public int TodoId { get; set; } = 0;

		[Required]
		public string Name { get; set; } = string.Empty;

		[Required]
		public string Desc { get; set; } = string.Empty;

        public string Tegs { get; set; } = string.Empty;

		[Required]
		public bool Done { get; set; } = false;
	}
}
