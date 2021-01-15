using AutoMapper;
using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.Repository;
using Hahn.ApplicatonProcess.December2020.Web.Dtos;
using Hahn.ApplicatonProcess.December2020.Web.Utils;
using static Hahn.ApplicatonProcess.December2020.Web.Utils.WebConstants.ApplicationResources;
using static Hahn.ApplicatonProcess.December2020.Web.Utils.WebConstants.ResponseMessages;
using static Hahn.ApplicatonProcess.December2020.Web.Utils.WebConstants.ModelProperties;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers.Applicants.Get
{
    public class Request : IRequest<ApiResponse>
    {
        public int ApplicantId { get; set; }
    }

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(it => it.ApplicantId)
                .GreaterThan(0);
        }
    }

    public class Handler : IRequestHandler<Request, ApiResponse>
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public Handler(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var applicant = await context.Applicant
                .AsNoTracking()
                .FirstOrDefaultAsync(it => it.ID == request.ApplicantId);

            if (applicant == null)
            {
                throw new ApiException(DOES_NOT_EXISTS(APPLICANT, ID, request.ApplicantId.ToString()),
                    StatusCodes.Status404NotFound);
            }

            return new ApiResponse(mapper.Map<ApplicantResponse>(applicant));
        }
    }
}
