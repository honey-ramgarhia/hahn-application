using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Web.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static Hahn.ApplicatonProcess.December2020.Domain.Constants.Validations;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Repository;
using Hahn.ApplicatonProcess.December2020.Web.Services;
using Swashbuckle.AspNetCore.Filters;
using Hahn.ApplicatonProcess.December2020.Web.Utils;
using static Hahn.ApplicatonProcess.December2020.Web.Utils.WebConstants.ResponseMessages;
using static Hahn.ApplicatonProcess.December2020.Web.Utils.WebConstants.Regexps;
using static Hahn.ApplicatonProcess.December2020.Web.Utils.WebConstants.ApplicationResources;
using Microsoft.AspNetCore.Http;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers.Applicants.Add
{
    public class Request : IRequest<ApiResponse>
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EMailAddress { get; set; }
        public int Age { get; set; }
        public bool IsHired { get; set; }
    }

    public class RequestEample : IExamplesProvider<Request>
    {
        public Request GetExamples()
        {
            return new Request
            {
                Name = "Harshwardhan",
                FamilyName = "Singh",
                Address = "Patiala, Punjab",
                CountryOfOrigin = "india",
                EMailAddress = "ramgarhia.singh99@gmail.com",
                Age = 26,
                IsHired = true
            };
        }
    }

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(it => it.Name)
                .NotEmpty()
                .Length(MIN_LENGTH, MAX_LENGTH);

            RuleFor(it => it.FamilyName)
                .NotEmpty()
                .Length(MIN_LENGTH, MAX_LENGTH);

            RuleFor(it => it.Address)
                .NotEmpty()
                .Length(MIN_LENGTH_ADDRESS, MAX_LENGTH_ADDRESS);

            RuleFor(it => it.CountryOfOrigin)
                .NotEmpty()
                .MaximumLength(MAX_LENGTH);

            RuleFor(it => it.EMailAddress)
                .NotEmpty()
                .MaximumLength(MAX_LENGTH)
                .Matches(VALID_EMAIL);

            RuleFor(it => it.Age)
                .InclusiveBetween(MIN_AGE, MAX_AGE);
        }
    }

    public class Handler : IRequestHandler<Request, ApiResponse>
    {
        private readonly AppDbContext context;
        private readonly CountryService countryService;
        private readonly IMapper mapper;
        private readonly IApplicationHttpContextService applicationHttpContextService;

        public Handler(AppDbContext context, CountryService countryService, IMapper mapper,
            IApplicationHttpContextService applicationHttpContextService)
        {
            this.context = context;
            this.countryService = countryService;
            this.mapper = mapper;
            this.applicationHttpContextService = applicationHttpContextService;
        }

        public async Task<ApiResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            if (! await countryService.Exists(request.CountryOfOrigin))
            {
                throw new ApiException(INVALID_COUNTRY_NAME, StatusCodes.Status400BadRequest);
            }

            Applicant applicant = new(request.Name, request.FamilyName, request.Address, request.CountryOfOrigin,
                request.EMailAddress, request.Age, request.IsHired);

            context.Add(applicant);
            await context.SaveChangesAsync();

            return new ApiResponse(mapper.Map<ApplicantResponse>(applicant), 
                $"{applicationHttpContextService.GetApplicationBaseUrl()}/{APPLICANT}/{applicant.ID}");
        }
    }
}
