using System.ComponentModel.DataAnnotations;
using Seiun.Resources;


namespace Seiun.Models.Parameters;

public class UserSelectTag
{
    [Required(ErrorMessage = ErrorMessages.ValidationError.UserIdRequired)]
    public required Guid TagId { get; set; }

    [Required(ErrorMessage = ErrorMessages.ValidationError.UserTagDailyPlanRequired)]
    public required int DailyPlan { get; set; }

    [Required(ErrorMessage = ErrorMessages.ValidationError.UserTagTotalDaysRequired)]
    public required int TotalDays { get; set; }
}