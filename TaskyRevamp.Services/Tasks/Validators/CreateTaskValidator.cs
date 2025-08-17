using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;

using TaskyRevamp.Services.Tasks.Commands;
namespace TaskyRevamp.Dto.TaskDto;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator(IStringLocalizer<CreateTaskValidator> localizer)
    {
        RuleFor(x => x.CreateTaskDto.TitleEnglish)
            .NotEmpty().WithMessage(localizer["TitleRequired"])
            .MaximumLength(100).WithMessage(localizer["TitleMaxLength"]);
        RuleFor(x => x.CreateTaskDto.TitleArabic)
    .NotEmpty().WithMessage(localizer["TitleRequired"])
    .MaximumLength(100).WithMessage(localizer["TitleMaxLength"]);
    }
}