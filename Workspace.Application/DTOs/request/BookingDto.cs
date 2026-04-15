using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace_Managment_System.identity;

namespace Workspace.Application.DTOs.request
{
    public class BookingDto : IValidatableObject
    {
        public string UserId { get; set; }
        public long RoomId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int CountOfPeople { get; set; }
        public string Mode { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartTime.ToLocalTime() < DateTime.Now)
            {
                yield return new ValidationResult(
                    "Start time must be later than current time.",
                    new[] { nameof(StartTime) }
                );
            }
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "End time must be later than start time.",
                    new[] { nameof(EndTime) }
                );
            }
            else if ((EndTime - StartTime).TotalMinutes < 30)
            {
                yield return new ValidationResult(
                    "The minimum booking duration is 30 minutes.",
                    new[] { nameof(EndTime) }
                );
            }
        }
    }
}
